using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using BackendConfortTravel.Models;
using BackendConfortTravel.Servicios;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FELController : ControllerBase

    {
        private readonly ConfortTravelContext context;
        private readonly HttpClient _httpClient;
        private string _authorizationToken; // Variable para almacenar el token dinámicamente
        private const string TaxId = "000112571913";
        private const string Data1 = "SHARED_GETINFONITcom";

        public FELController(ConfortTravelContext context, HttpClient httpClient)
        {
            this.context = context;
            _httpClient = httpClient;
        }

        private async Task<bool> GenerateTokenAsync()
        {
            try
            {
                string url = "https://felgtaws.digifact.com.gt/gt.com.apinuc/api/login/get_token";
                var loginData = new
                {
                    Username = "GT.000112571913.joselin",
                    Password = "K$_d3&4&"
                };

                string jsonContent = JsonSerializer.Serialize(loginData);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonSerializer.Deserialize<TokenResponse>(responseData);
                    _authorizationToken = tokenData?.Token; // Guarda el token recibido
                    return true; // Token generado con éxito
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return false; // Error al generar el token
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false; // Error interno
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClientInfoAsync(string nit)
        {
            try
            {
                // Generar el token si no está disponible
                if (string.IsNullOrEmpty(_authorizationToken))
                {
                    bool tokenGenerated = await GenerateTokenAsync();
                    if (!tokenGenerated)
                    {
                        return StatusCode(500, "Error al generar el token de autorización");
                    }
                }

                string url = $"https://felgtaws.digifact.com.gt/gt.com.apinuc/api/Shared?TAXID={TaxId}&DATA1={Data1}&DATA2=NIT|{nit}&USERNAME=joselin";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", _authorizationToken);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, "Error al obtener los datos del cliente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFELAsync(int nit, string nombre, int idCotizacion)
        {
            try
            {
                // Generar el token si no está disponible
                if (string.IsNullOrEmpty(_authorizationToken))
                {
                    bool tokenGenerated = await GenerateTokenAsync();
                    if (!tokenGenerated)
                    {
                        return StatusCode(500, "Error al generar el token de autorización");
                    }
                }
                string issuedDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"); // Obtiene la fecha y hora actual del sistema

                var servicio = context.TblCotizacions
                    .Include(i => i.IdPaqueteViajeNavigation)
                    .Select(i => new
                    {
                        IdCotizacion = i.IdCotizacion,
                        // Formato de hasta 6 cifras decimales, asegurando que use la cultura adecuada para el formato.
                        Precio = Math.Round((decimal)i.PrecioCotizacion, 6).ToString("F6", CultureInfo.InvariantCulture),
                MontoGravable = Math.Round((double)i.PrecioCotizacion / 1.12d, 6).ToString("F6", CultureInfo.InvariantCulture),
                       MontoImpuesto = Math.Round((double)i.PrecioCotizacion * 0.12d, 6).ToString("F6", CultureInfo.InvariantCulture),
                Descripcion = i.IdPaqueteViajeNavigation.Titulo,
                       
                        CANTIDAD_LETRAS = NumberToWordsConverter.ConvertToWords((decimal)i.PrecioCotizacion) // Generar cantidad en letras

                    })
                    .FirstOrDefault(i => i.IdCotizacion == idCotizacion);

                string url = $"https://felgtaws.digifact.com.gt/gt.com.apinuc/api/v2/transform/nuc?TAXID={TaxId}&FORMAT=PDF|XML&USERNAME=joselin";
                // Construir el XML con la información dinámica
                string xmlContent = 
               $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <Root>
                        <Version>1.00</Version>
                        <CountryCode>GT</CountryCode>
                        <Header>
                            <DocType>FACT</DocType> <!-- DatosGenerales@Tipo-->
                            <IssuedDateTime>{issuedDateTime}</IssuedDateTime> <!-- DatosGenerales@FechaHoraEmision --> 
                            <Currency>GTQ</Currency> <!-- DatosGenerales@CodigoMoneda  -->  
                        </Header>
                        <Seller>
                            <TaxID>112571913</TaxID> <!-- Emisor@NITEmisor -->
                            <TaxIDAdditionalInfo>
                                <Info Name=""AfiliacionIVA"" Value=""GEN""/> <!-- Emisor@AfiliacionIVA -->
                            </TaxIDAdditionalInfo>
                            <Name>CONFORT TRAVEL</Name> <!-- Emisor@NombreEmisor -->
                            <AdditionlInfo> <!-- FRASES -->
                                <Info Name=""TipoFrase"" Data=""1"" Value=""1""/>
                                <Info Name=""Escenario"" Data=""1"" Value=""1""/>
                            </AdditionlInfo>
                            <BranchInfo>
                                <Code>1</Code> <!-- Emisor@CodigoEstablecimiento -->
                                <Name>CONFORT TRAVEL</Name> <!-- Emisor@NombreComercial -->
                                <AddressInfo> <!-- DireccionEmisor -->
                                    <Address>Poptún, Petén</Address> <!-- Direccion -->
                                    <City>1701</City>      <!-- CodigoPostal -->
                                    <District>Poptún</District>  <!-- Municipio -->
                                    <State>Petén</State>   <!-- Departamento -->
                                    <Country>GT</Country>       <!-- Pais -->
                                </AddressInfo>
                            </BranchInfo>
                        </Seller>
                        <Buyer>
                            <TaxID>{nit}</TaxID> <!-- Receptor@IDReceptor -->
                            <!-- si la casilla IDReceptor contiene el NIT, debe contener el nombre asociado al NIT, caso contrario puede ser cualquier cosa -->
                            <Name>{nombre}</Name> <!-- Receptor@NombreReceptor -->
                            <AddressInfo>   <!-- DireccionReceptor -->
                                <Address>CIUDAD</Address>        <!-- Direccion -->
                                <City>1701</City>          <!-- CodigoPostal -->
                                <District>Flores</District>  <!-- Municipio -->
                                <State>Petén</State>        <!-- Departamento -->
                                <Country>GT</Country>    <!-- Pais -->
                            </AddressInfo>
                        </Buyer>
                        <Items>
                            <Item> 
                                <Type>servicio</Type> <!-- Item@BienOServicio -->
                                <Description>{servicio.Descripcion}</Description> <!-- Descripcion -->
                                <Qty>1.000000</Qty> <!-- Cantidad -->
                                <UnitOfMeasure>UNO</UnitOfMeasure> <!-- UnidadMedida -->
                                <Price>{servicio.Precio}</Price> <!-- PrecioUnitario | precio sin impuesto -->
                                <Taxes> <!-- Impuestos -->
                                    <Tax> <!-- Impuesto -->
                                        <Code>1</Code>  <!-- CodigoUnidadGravable -->
                                        <Description>IVA</Description> <!-- NombreCorto -->
                                        <TaxableAmount>{servicio.MontoGravable}</TaxableAmount> <!-- MontoGravable -->
                                        <Amount>{servicio.MontoImpuesto}</Amount> <!-- MontoImpuesto -->
                                    </Tax>
                                </Taxes>
                                <Totals> <!-- Total -->
                                    <TotalItem>{servicio.Precio}</TotalItem> <!-- Total -->
                                </Totals>
                            </Item>
                        </Items>
                        <Totals> <!-- Totales -->
                            <TotalTaxes> <!-- TotalImpuestos -->
                                <TotalTax>  <!-- TotalImpuesto -->
                                    <Description>IVA</Description>  <!-- TotalImpuesto@NombreCorto -->
                                    <Amount>{servicio.MontoImpuesto}</Amount>      <!-- TotalImpuesto@TotalMontoImpuesto -->
                                </TotalTax>
                            </TotalTaxes>
                            <GrandTotal>
                                <InvoiceTotal>{servicio.Precio}</InvoiceTotal> <!-- GranTotal -->
                            </GrandTotal>
                            <!-- <InWords></InWords> -->
                        </Totals>
                        <AdditionalDocumentInfo>
                            <!-- ADENDA -->
                            <AdditionalInfo>
                                <Code>FRONT-263C-444B-89BA-6F87EC1330C0</Code> <!-- REFERENCIA_INTERNA -->
                                <Type>ADENDA</Type>
                                <AditionalData>
                                    <!-- INFORMACION_ADICIONAL -->
                                    <Data Name=""INFORMACION_ADICIONAL"">
                                        <Info Name=""OBSERVACIONES"" Value=""-""/>
                                        <Info Name=""CANTIDAD_LETRAS"" Value=""{servicio.CANTIDAD_LETRAS}""/>
                                    </Data>
                                    <!-- Detalles_Auxiliares, pueden venir muchos DetallesAux_Detalle-->
                                    <Data Name=""DetallesAux_Detalle"">
                                        <Info Name=""NumeroLinea"" Value=""1""/>
                                        <Info Name=""Descripcion_Adicional"" Value=""-""/>
                                        <Info Name=""CodigoEAN"" Value=""00015""/>
                                        <Info Name=""CategoriaAdicional"" Value=""-""/>
                                    </Data>
                                </AditionalData>
                                <AditionalInfo>
                                    <Info Name=""VALIDAR_REFERENCIA_INTERNA"" Value=""NO_VALIDAR""/>
                                </AditionalInfo>
                            </AdditionalInfo>
                        </AdditionalDocumentInfo>
                    </Root>";

                HttpContent content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = content;
                request.Headers.Add("Authorization", _authorizationToken);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, "Error al obtener los datos del cliente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }

        //Registra el FEL en la base de datos
        [HttpPost("registrar-fel-bd")]
        public ActionResult RegistrarFelBd([FromBody] TblFel fel)

        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblFels.Any(p => p.IdOrdenDePago == fel.IdOrdenDePago);
                if (Existe)
                {
                    return new BadRequestObjectResult("Ya se ha registrado este FEL");
                }
                int? maxId = context.TblFels.Max(p => (int?)p.IdFel);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
               
                fel.IdFel = nuevoId;
                fel.Fecha = DateTime.Now;
                fel.IdImpuesto = 1;
                fel.Moneda = "GTQ";
                fel.Estado = true;

                context.TblFels.Add(fel);
                context.SaveChanges();

                var oPago = context.TblOrdenDePagos.FirstOrDefault(
                    i => i.IdOrdenDePago == fel.IdOrdenDePago
                        );
                oPago.Estado = false;
                context.SaveChanges();

                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //para mostrar el detalle de un pago, esto es para el area de administracion
        [HttpGet("GetDetallePago")]
        public ActionResult GetDetallePago(int idOrdenDePago)
        {
            try
            {
                var detallePago = context.TblFels.FirstOrDefault(i => i.IdOrdenDePago == idOrdenDePago);
                return Ok(detallePago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }


    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
