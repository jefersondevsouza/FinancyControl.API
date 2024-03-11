using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FinancyControl.API.Domain.Services;
using FinancyControl.API.Resources;
using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;

namespace FinancyControl.API.Controllers
{
    public class DespesaController : BaseApiController
    {
        private readonly IDespesaService _DespesaService;
        private readonly IMapper _mapper;

        public DespesaController(IDespesaService DespesaService, IMapper mapper)
        {
            _DespesaService = DespesaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all existing Despesa according to query filters.
        /// </summary>
        /// <returns>List of Despesa.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<DespesaResource>), 200)]
        public async Task<QueryResultResource<DespesaResource>> ListAsync([FromQuery] DespesaQueryResource query)
        {
            var despesaQuery = _mapper.Map<DespesaQuery>(query);
            var queryResult = await _DespesaService.ListAsync(despesaQuery);

            return _mapper.Map<QueryResultResource<DespesaResource>>(queryResult);
        }

        /// <summary>
        /// Saves a new Despesa.
        /// </summary>
        /// <param name="resource">Despesa data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(DespesaResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveDespesaResource resource)
        {
            var despesa = _mapper.Map<Despesa>(resource);
            var result = await _DespesaService.SaveAsync(despesa);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var DespesaResource = _mapper.Map<DespesaResource>(result.Resource!);
            return Ok(DespesaResource);
        }

        /// <summary>
        /// Updates an existing Despesa according to an identifier.
        /// </summary>
        /// <param name="id">Despesa identifier.</param>
        /// <param name="resource">Despesa data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DespesaResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDespesaResource resource)
        {
            var despesa = _mapper.Map<Despesa>(resource);
            var result = await _DespesaService.UpdateAsync(id, despesa);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var DespesaResource = _mapper.Map<DespesaResource>(result.Resource!);
            return Ok(DespesaResource);
        }

        /// <summary>
        /// Deletes a given Despesa according to an identifier.
        /// </summary>
        /// <param name="id">Despesa identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DespesaResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _DespesaService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var despesaResource = _mapper.Map<DespesaResource>(result.Resource!);
            return Ok(despesaResource);
        }
    }
}
