using AutoMapper;
using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.AdditionalServices;
using DEBUG.BL.Services.TagServices;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;
using DEBUG.Core.RepositoryInstances;
using Microsoft.AspNetCore.Identity;

namespace DEBUG.BL.Services.QuestionServices;

public class QuestionService(IQuestionRepository _repository, ITagService _tagService, ILikeDislikeService _likeService, UserManager<User> _userManager, IMapper _mapper) : IQuestionService
{
    public async Task<int> CreateAsync(int CategoryId, QuestionCreateDTO dto, User user)
    {
        Question question = _mapper.Map<Question>(dto);
        question.UserId = user.Id;
        question.CategoryId = CategoryId;
        question.Tags = await _tagService.GetRangeByIdsAsync(dto.TagIds); ;
        await _repository.CreateAsync(question);

        user.QuestionCount++;
        if (user.QuestionCount >= 25)
        {
            user.Badges |= Badges.Question25;
            user.Badges &= ~Badges.Question10;
        }
        else if (user.QuestionCount >= 10)
            user.Badges |= Badges.Question10;

        await _repository.SaveChangesAsync();
        return question.Id;
    }

    public async Task<IEnumerable<QuestionGetDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<QuestionGetDTO>>(await _repository.GetWhereAsync(x => x.IsDeleted == false, ["User", "Category", "Tags"]));
    }

    public async Task<QuestionGetDTO> GetByIdAsync(int id)
    {
        Question? question = await _repository.GetByIdAsync(id, x => x.IsDeleted == false, ["User", "Category", "Tags"]);
        if (question == null) throw new NotFoundException<Question>();
        return _mapper.Map<QuestionGetDTO>(question);
    }

    public async Task<IEnumerable<QuestionGetDTO>> GetByUserIdAsync(string id)
    {
        if (await _userManager.FindByIdAsync(id) == null) throw new NotFoundException<User>();
        return _mapper.Map<IEnumerable<QuestionGetDTO>>(await _repository.GetWhereAsync(x => x.UserId == id, ["User", "Category", "Tags"]));
    }
    public async Task HardDeleteAsync(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Question>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task LikeDislikeAsync(User user, int questionId, bool isLiked)
    {
        await _likeService.LikeDislikeItemAsync(user, questionId, LikedEntityTypes.Question, isLiked);
    }
    public async Task<LikeDislikeDTO> GetLikeDislikeAsync(int questionId)
    {
        return await _likeService.GetLikeDislikeCountAsync(questionId, LikedEntityTypes.Question);
    }

    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Question>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task<QuestionGetDTO> UpdateAsync(int id, QuestionUpdateDTO dto)
    {
        Question? question = await _repository.GetByIdAsync(id);
        if (question == null) throw new NotFoundException<Question>();
        _mapper.Map(dto, question);
        await _repository.SaveChangesAsync();
        return _mapper.Map<QuestionGetDTO>(question);
    }
}
