﻿namespace CalorieTracker.Domains;

public interface IBaseEntity
{
    public int Id { get; set; }
}

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}