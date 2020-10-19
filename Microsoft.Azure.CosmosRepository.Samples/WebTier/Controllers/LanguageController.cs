﻿// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using WebTier.Models;

namespace WebTier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        readonly IRepository<Language> _repository;

        public LanguageController(IRepositoryFactory factory) =>
            _repository = factory.RepositoryOf<Language>();

        [HttpGet(Name = nameof(GetLanguages))]
        public Task<IEnumerable<Language>> GetLanguages() =>
            _repository.GetAsync(l => l.InitialReleaseDate > new DateTime(1984, 7, 7));

        [HttpGet("{id}", Name = nameof(GetLanguageById))]
        public Task<Language> GetLanguageById(string id) =>
            _repository.GetAsync(id);

        [HttpPost(Name = nameof(PostLanguages))]
        public Task<IEnumerable<Language>> PostLanguages([FromBody] params Language[] languages) =>
            _repository.CreateAsync(languages);

        [HttpPut(Name = nameof(PutLanguage))]
        public Task<Language> PutLanguage([FromBody] Language language) =>
            _repository.UpdateAsync(language);

        [HttpDelete("{id}", Name = nameof(DeleteLanguage))]
        public Task DeleteLanguage(string id) =>
            _repository.DeleteAsync(id);
    }
}
