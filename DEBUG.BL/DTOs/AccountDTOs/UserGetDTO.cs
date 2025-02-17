﻿namespace DEBUG.BL.DTOs.AccountDTOs;

public class UserGetDTO
{
    public string Id { get; set; }
    public string ProfileImage { get; set; }
    public string UserName { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
}
