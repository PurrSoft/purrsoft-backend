using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Persistence.Repositories;

public class AnimalFosterMapRepository(PurrSoftDbContext context) : Repository<AnimalFosterMap>(context), IAnimalFosterMapRepository
{
	public Task<IEnumerable<AnimalFosterMap>> GetAnimalFosterMapsForFoster(string fosterId, CancellationToken cancellationToken)
	{
		return Task.FromResult(DbContext.AnimalFosters.
			Where(af => af.FosterId == fosterId).
			AsEnumerable()
		);
	}

	public Task<IEnumerable<AnimalFosterMap>> GetAnimalFosterMapsForAnimal(string animalId, CancellationToken cancellationToken)
	{
		return Task.FromResult(DbContext.AnimalFosters.
			Where(af => af.AnimalId.ToString() == animalId).
			AsEnumerable()
		);
	}

	public Task<bool> IsAnimalAssignedToFoster(string animalId, string fosterId, CancellationToken cancellationToken)
	{
		return Task.FromResult(DbContext.AnimalFosters
			.Any(af => af.AnimalId.ToString() == animalId
				&& af.FosterId == fosterId));
	}
}