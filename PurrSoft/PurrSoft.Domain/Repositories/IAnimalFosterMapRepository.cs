using PurrSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Domain.Repositories;

public interface IAnimalFosterMapRepository : IRepository<AnimalFosterMap>
{
	public Task<IEnumerable<AnimalFosterMap>> GetAnimalFosterMapsForFoster(string fosterId, CancellationToken cancellationToken);

	public Task<IEnumerable<AnimalFosterMap>> GetAnimalFosterMapsForAnimal(string animalId, CancellationToken cancellationToken);

	public Task<bool> IsAnimalAssignedToFoster(string animalId, string fosterId, CancellationToken cancellationToken);
}
