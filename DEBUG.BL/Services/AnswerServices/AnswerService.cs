using AutoMapper;
using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.BL.Services.AdditionalServices;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;
using DEBUG.Core.RepositoryInstances;
using Microsoft.AspNetCore.Identity;

namespace DEBUG.BL.Services.AnswerServices;

public class AnswerService(IAnswerRepository _repository, UserManager<User> _userManager, ILikeDislikeService _likeService, IMapper _mapper) : IAnswerService
{
    public async Task<int> CreateAsync(int questionId, AnswerCreateDTO dto, User user)
    {
        Answer Answer = _mapper.Map<Answer>(dto);
        Answer.QuestionId = questionId;
        Answer.UserId = user.Id;
        await _repository.CreateAsync(Answer);

        user.AnswersCount++;
        if (user.AnswersCount >= 25)
        {
            user.Badges |= Badges.Answer25;
            user.Badges &= ~Badges.Answer10;
        }
        else if (user.AnswersCount >= 10)
            user.Badges |= Badges.Answer10;

        await _repository.SaveChangesAsync();
        return Answer.Id;
    }
    public async Task<IEnumerable<AnswerGetDTO>> GetAllByQuestionIdAsync(int questionId)
        => _mapper.Map<IEnumerable<AnswerGetDTO>>(await _repository.GetWhereAsync(x => x.IsDeleted == false && x.QuestionId == questionId, "User"));
    public async Task<AnswerGetDTO> GetByIdAsync(int id)
    {
        Answer? answer = await _repository.GetByIdAsync(id, x => x.IsDeleted == false, "User");
        if (answer == null) throw new NotFoundException<Answer>();
        return _mapper.Map<AnswerGetDTO>(answer);
    }
    public async Task<IEnumerable<AnswerGetDTO>> GetByUserIdAsync(string id)
    {
        if (await _userManager.FindByIdAsync(id) == null) throw new NotFoundException<User>();
        return _mapper.Map<IEnumerable<AnswerGetDTO>>(await _repository.GetWhereAsync(x => x.UserId == id, "User"));
    }
    public async Task HardDeleteAsync(int id)
    {
        Answer? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Answer>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task LikeDislikeAsync(User user, int answerId, bool isLiked)
        => await _likeService.LikeDislikeItemAsync(user, answerId, LikedEntityTypes.Answer, isLiked);
    public async Task<LikeDislikeDTO> GetLikeDislikeAsync(int answerId)
        => await _likeService.GetLikeDislikeCountAsync(answerId, LikedEntityTypes.Answer);
    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Answer? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Answer>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }
    public async Task<AnswerGetDTO> UpdateAsync(int id, AnswerUpdateDTO dto)
    {
        Answer? answer = await _repository.GetByIdAsync(id);
        if (answer == null) throw new NotFoundException<Answer>();
        _mapper.Map(dto, answer);
        await _repository.SaveChangesAsync();
        return _mapper.Map<AnswerGetDTO>(answer);
    }
}