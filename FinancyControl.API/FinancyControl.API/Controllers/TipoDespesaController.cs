using AutoMapper;
using FinancyControl.API.Resources;
using Microsoft.AspNetCore.Mvc;
using FinancyControl.API.Domain.Services;
using FinancyControl.API.Resources;
using FinancyControl.API.Domain.Models;

namespace FinancyControl.API.Controllers
{
    public class TipoDespesaController : BaseApiController
    {
        private readonly ITipoDespesaService _TipoDespesaService;
        private readonly IMapper _mapper;

        public TipoDespesaController(ITipoDespesaService TipoDespesaService, IMapper mapper)
        {
            _TipoDespesaService = TipoDespesaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos dos tipos de despesas.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoDespesaResource>), 200)]
        public async Task<IEnumerable<TipoDespesaResource>> ListAsync()
        {
            var tiposDespesa = await _TipoDespesaService.ListAsync();
            return _mapper.Map<IEnumerable<TipoDespesaResource>>(tiposDespesa);
        }

        /// <summary>
        /// Sava um novo tipo de despesa.
        /// </summary>
        /// <param name="resource">TipoDespesa data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TipoDespesaResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTipoDespesaResource resource)
        {
            var TipoDespesa = _mapper.Map<TipoDespesa>(resource);
            var result = await _TipoDespesaService.SaveAsync(TipoDespesa);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var TipoDespesaResource = _mapper.Map<TipoDespesaResource>(result.Resource!);
            return Ok(TipoDespesaResource);
        }

        /// <summary>
        /// Updates an existing TipoDespesa according to an identifier.
        /// </summary>
        /// <param name="id">TipoDespesa identifier.</param>
        /// <param name="resource">Updated TipoDespesa data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TipoDespesaResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTipoDespesaResource resource)
        {
            var tipoDespesa = _mapper.Map<TipoDespesa>(resource);
            var result = await _TipoDespesaService.UpdateAsync(id, tipoDespesa);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var TipoDespesaResource = _mapper.Map<TipoDespesaResource>(result.Resource!);
            return Ok(TipoDespesaResource);
        }

        /// <summary>
        /// Deletes a given TipoDespesa according to an identifier.
        /// </summary>
        /// <param name="id">TipoDespesa identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TipoDespesaResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _TipoDespesaService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var TipoDespesaResource = _mapper.Map<TipoDespesaResource>(result.Resource!);
            return Ok(TipoDespesaResource);
        }
    }
}
