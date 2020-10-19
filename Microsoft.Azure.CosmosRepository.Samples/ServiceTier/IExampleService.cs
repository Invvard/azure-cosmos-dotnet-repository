// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceTier
{
    public interface IExampleService
    {
        Task<Person> AddPersonAsync(Person person);
        Task<IEnumerable<Person>> AddPeopleAsync(IEnumerable<Person> people);

        Task<Person> ReadPersonByIdAsync(string id, string partitionKey);
        Task<IEnumerable<Person>> ReadPeopleAsync(Expression<Func<Person, bool>> matches);

        Task<Person> UpdatePersonAsync(Person person);

        Task DeletePersonAsync(Person person);
    }
}