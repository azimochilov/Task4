using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task4.Data.IRepositories;
using Task4.Domain.Entities;
using Task4.Domain.Enums;
using Task4.Service.DTOs;
using Task4.Service.Exceptions;
using Task4.Service.Interfaces;

namespace Task4.Service.Services;
public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> repository;

    public UserService(IMapper mapper, IRepository<User> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<UserForResultDto> AddAsync(UserForCreationDto userForCreationDto)
    {
        var exsistEntity = await repository.SelectAsync(x => x.Email == userForCreationDto.Email && !x.IsDeleted);
        if(exsistEntity is null)
        {
            return null;
        }

        var mapped = mapper.Map<User>(userForCreationDto);
        var addedModel = await repository.InsertAsync(mapped);
        await repository.SaveAsync();
        
        return mapper.Map<UserForResultDto>(addedModel);
    }

    public async Task<UserForResultDto> BlockAsync(long id)
    {
        var exsistEntity = await repository.SelectAsync(x => x.Id == id);
        if(exsistEntity is null)
        {
            throw new TaskException(404, "User not found");
        }

        exsistEntity.Type = Status.Blocked;
        exsistEntity.LastLoginTime = DateTime.Now;

        var mapped = mapper.Map<UserForResultDto>(exsistEntity);

        await repository.SaveAsync();
        return mapped;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exsistEntity = await repository.SelectAsync(x =>x.Id == id);
        if (exsistEntity is null || exsistEntity.IsDeleted)
            throw new TaskException(404, "Couldn't find user for this given Id");

        return await repository.DeleteAsync(x => x.Id == exsistEntity.Id);
    }

    public async Task<UserForResultDto> UnBlockAsync(long id)
    {
        var exsistEntity = await repository.SelectAsync(x => x.Id == id);
        if (exsistEntity is null)
        {
            throw new TaskException(404, "User not found");
        }

        exsistEntity.Type = Status.Active;
        exsistEntity.LastLoginTime = DateTime.Now;

        var mapped = mapper.Map<UserForResultDto>(exsistEntity);

        await repository.SaveAsync();
        return mapped;
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync()
    {
        var users = await repository.SelectAll().OrderBy(u => u.Id).ToListAsync();

        return mapper.Map<IEnumerable<UserForResultDto>>(users);
    }
}
