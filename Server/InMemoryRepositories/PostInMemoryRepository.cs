﻿using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task<Post> UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost != null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }
        
        posts.Remove(existingPost);
        
        posts.Add(post);
        return Task.FromResult(existingPost);
    }

    public Task<Post> DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove != null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.FromResult(postToRemove);
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}