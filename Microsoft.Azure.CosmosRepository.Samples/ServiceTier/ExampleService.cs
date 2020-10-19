// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.CosmosRepository;

namespace ServiceTier
{
    public class ExampleService : IExampleService
    {
        readonly IRepository<Person> _personRepository;

        public ExampleService(IRepository<Person> personRepository) =>
            _personRepository = personRepository;

        public Task<IEnumerable<Person>> AddPeopleAsync(IEnumerable<Person> people) =>
            _personRepository.CreateAsync(people);

        public Task<Person> AddPersonAsync(Person person) =>
            _personRepository.CreateAsync(person);

        public Task DeletePersonAsync(Person person) =>
            _personRepository.DeleteAsync(person);

        public Task<IEnumerable<Person>> ReadPeopleAsync(Expression<Func<Person, bool>> matches) =>
            _personRepository.GetAsync(matches);

        public Task<Person> ReadPersonByIdAsync(string id, string partitionKey) =>
            _personRepository.GetAsync(id, partitionKey);

        public Task<Person> UpdatePersonAsync(Person person) =>
            _personRepository.UpdateAsync(person);
    }
}
