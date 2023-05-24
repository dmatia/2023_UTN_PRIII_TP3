using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
	public interface IAtributosNegocio
	{
		List<IAtributo> listar();
		bool agregar(IAtributo atributo);
		bool modificar(IAtributo atributos);
		bool eliminar(IAtributo atributos);
	}
}
