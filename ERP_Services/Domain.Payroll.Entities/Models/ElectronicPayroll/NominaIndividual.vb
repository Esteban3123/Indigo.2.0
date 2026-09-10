Imports System.Xml.Serialization

<XmlRoot("NominaIndividual", [Namespace]:="dian:gov:co:facturaelectronica:NominaIndividual")>
Public Class NominaIndividual
    <XmlElement("Novedad")> Public Property Novedad As Novedad
    <XmlElement("Periodo")> Public Property Periodo As Periodo
    <XmlElement("NumeroSecuenciaXML")> Public Property NumeroSecuenciaXML As NumeroSecuenciaXML
    <XmlElement("LugarGeneracionXML")> Public Property LugarGeneracionXML As LugarGeneracionXML
    <XmlElement("ProveedorXML")> Public Property ProveedorXML As ProveedorXML
    <XmlElement("CodigoQR")> Public Property CodigoQR As String
    <XmlElement("InformacionGeneral")> Public Property InformacionGeneral As InformacionGeneral
    <XmlElement("Notas")> Public Property Notas As String
    <XmlElement("Empleador")> Public Property Empleador As Empleador
    <XmlElement("Trabajador")> Public Property Trabajador As Trabajador
    <XmlElement("Pago")> Public Property Pago As Pago
    <XmlElement("FechasPagos")> Public Property FechasPagos As FechasPagos
    <XmlElement("Devengados")> Public Property Devengados As Devengados
    <XmlElement("Deducciones")> Public Property Deducciones As Deducciones
    <XmlElement("Redondeo")> Public Property Redondeo As String
    <XmlElement("DevengadosTotal")> Public Property DevengadosTotal As Decimal
    <XmlElement("DeduccionesTotal")> Public Property DeduccionesTotal As Decimal
    <XmlElement("ComprobanteTotal")> Public Property ComprobanteTotal As Decimal
    <XmlIgnore> Public Property ReportConcept As List(Of NominaConcepto)
End Class

Public Class Novedad
    <XmlAttribute("CUNENov")> Public Property CUNENov As String
    <XmlText> Public Property Value As Boolean
End Class

Public Class Periodo
    <XmlAttribute("FechaIngreso")> Public Property FechaIngreso As String
    <XmlAttribute("FechaRetiro")> Public Property FechaRetiro As String
    <XmlAttribute("FechaLiquidacionInicio")> Public Property FechaLiquidacionInicio As String
    <XmlAttribute("FechaLiquidacionFin")> Public Property FechaLiquidacionFin As String
    <XmlAttribute("TiempoLaborado")> Public Property TiempoLaborado As String
    <XmlAttribute("FechaGen")> Public Property FechaGen As String
End Class

Public Class NumeroSecuenciaXML
    <XmlAttribute("CodigoTrabajador")> Public Property CodigoTrabajador As String
    <XmlAttribute("Prefijo")> Public Property Prefijo As String
    <XmlAttribute("Consecutivo")> Public Property Consecutivo As String
    <XmlAttribute("Numero")> Public Property Numero As String
End Class

Public Class LugarGeneracionXML
    <XmlAttribute("Pais")> Public Property Pais As String
    <XmlAttribute("DepartamentoEstado")> Public Property DepartamentoEstado As String
    <XmlAttribute("MunicipioCiudad")> Public Property MunicipioCiudad As String
    <XmlAttribute("Idioma")> Public Property Idioma As String
End Class

Public Class ProveedorXML
    <XmlAttribute("RazonSocial")> Public Property RazonSocial As String
    <XmlAttribute("PrimerApellido")> Public Property PrimerApellido As String
    <XmlAttribute("SegundoApellido")> Public Property SegundoApellido As String
    <XmlAttribute("PrimerNombre")> Public Property PrimerNombre As String
    <XmlAttribute("OtrosNombres")> Public Property OtrosNombres As String
    <XmlAttribute("NIT")> Public Property NIT As String
    <XmlAttribute("DV")> Public Property DV As String
    <XmlAttribute("SoftwareID")> Public Property SoftwareID As String
    <XmlAttribute("SoftwareSC")> Public Property SoftwareSC As String
End Class

Public Class InformacionGeneral
    <XmlAttribute("Version")> Public Property Version As String
    <XmlAttribute("Ambiente")> Public Property Ambiente As String
    <XmlAttribute("TipoXML")> Public Property TipoXML As String
    <XmlAttribute("CUNE")> Public Property CUNE As String
    <XmlAttribute("EncripCUNE")> Public Property EncripCUNE As String
    <XmlAttribute("FechaGen")> Public Property FechaGen As String
    <XmlAttribute("HoraGen")> Public Property HoraGen As String
    <XmlAttribute("PeriodoNomina")> Public Property PeriodoNomina As String
    <XmlAttribute("TipoMoneda")> Public Property TipoMoneda As String
    <XmlAttribute("TRM")> Public Property TRM As String
End Class

Public Class Empleador
    <XmlAttribute("RazonSocial")> Public Property RazonSocial As String
    <XmlAttribute("PrimerApellido")> Public Property PrimerApellido As String
    <XmlAttribute("SegundoApellido")> Public Property SegundoApellido As String
    <XmlAttribute("PrimerNombre")> Public Property PrimerNombre As String
    <XmlAttribute("OtrosNombres")> Public Property OtrosNombres As String
    <XmlAttribute("NIT")> Public Property NIT As String
    <XmlAttribute("DV")> Public Property DV As String
    <XmlAttribute("Pais")> Public Property Pais As String
    <XmlAttribute("DepartamentoEstado")> Public Property DepartamentoEstado As String
    <XmlAttribute("MunicipioCiudad")> Public Property MunicipioCiudad As String
    <XmlAttribute("Direccion")> Public Property Direccion As String
End Class

Public Class Trabajador
    <XmlAttribute("TipoTrabajador")> Public Property TipoTrabajador As String
    <XmlAttribute("SubTipoTrabajador")> Public Property SubTipoTrabajador As String
    <XmlAttribute("AltoRiesgoPension")> Public Property AltoRiesgoPension As Boolean
    <XmlAttribute("TipoDocumento")> Public Property TipoDocumento As String
    <XmlAttribute("NumeroDocumento")> Public Property NumeroDocumento As String
    <XmlAttribute("PrimerApellido")> Public Property PrimerApellido As String
    <XmlAttribute("SegundoApellido")> Public Property SegundoApellido As String
    <XmlAttribute("PrimerNombre")> Public Property PrimerNombre As String
    <XmlAttribute("OtrosNombres")> Public Property OtrosNombres As String
    <XmlAttribute("LugarTrabajoPais")> Public Property LugarTrabajoPais As String
    <XmlAttribute("LugarTrabajoDepartamentoEstado")> Public Property LugarTrabajoDepartamentoEstado As String
    <XmlAttribute("LugarTrabajoMunicipioCiudad")> Public Property LugarTrabajoMunicipioCiudad As String
    <XmlAttribute("LugarTrabajoDireccion")> Public Property LugarTrabajoDireccion As String
    <XmlAttribute("SalarioIntegral")> Public Property SalarioIntegral As Boolean
    <XmlAttribute("TipoContrato")> Public Property TipoContrato As String
    <XmlAttribute("Sueldo")> Public Property Sueldo As Decimal
    <XmlAttribute("CodigoTrabajador")> Public Property CodigoTrabajador As String
End Class

Public Class Pago
    <XmlAttribute("Forma")> Public Property Forma As String
    <XmlAttribute("Metodo")> Public Property Metodo As String
    <XmlAttribute("Banco")> Public Property Banco As String
    <XmlAttribute("TipoCuenta")> Public Property TipoCuenta As String
    <XmlAttribute("NumeroCuenta")> Public Property NumeroCuenta As String
End Class

Public Class FechasPagos
    <XmlElement("FechaPago")> Public Property FechaPago As List(Of String)
End Class

Public Class Devengados
    <XmlElement("Basico")> Public Property Basico As Basico
    <XmlElement("Transporte")> Public Property Transporte As Transporte
    <XmlArray("HEDs"), XmlArrayItem("HED")> Public Property HEDs As List(Of HED)
    <XmlArray("HENs"), XmlArrayItem("HEN")> Public Property HENs As List(Of HEN)
    <XmlArray("HRNs"), XmlArrayItem("HRN")> Public Property HRNs As List(Of HRN)
    <XmlArray("HEDDFs"), XmlArrayItem("HEDDF")> Public Property HEDDFs As List(Of HEDDF)
    <XmlArray("HRDDFs"), XmlArrayItem("HRDDF")> Public Property HRDDFs As List(Of HRDDF)
    <XmlArray("HENDFs"), XmlArrayItem("HENDF")> Public Property HENDFs As List(Of HENDF)
    <XmlArray("HRNDFs"), XmlArrayItem("HRNDF")> Public Property HRNDFs As List(Of HRNDF)
    <XmlElement("Vacaciones")> Public Property Vacaciones As Vacaciones
    <XmlElement("Primas")> Public Property Primas As Primas
    <XmlElement("Cesantias")> Public Property Cesantias As Cesantias
    <XmlArray("Incapacidades"), XmlArrayItem("Incapacidad")> Public Property Incapacidades As List(Of Incapacidad)
    <XmlElement("Licencias")> Public Property Licencias As Licencias
    <XmlArray("Bonificaciones"), XmlArrayItem("Bonificacion")> Public Property Bonificaciones As List(Of Bonificacion)
    <XmlArray("Auxilios"), XmlArrayItem("Auxilio")> Public Property Auxilios As List(Of Auxilio)
    <XmlArray("HuelgasLegales"), XmlArrayItem("HuelgaLegal")> Public Property HuelgasLegales As List(Of HuelgaLegal)
    <XmlArray("OtrosConceptos"), XmlArrayItem("OtroConcepto")> Public Property OtrosConceptos As List(Of OtroConcepto)
    <XmlArray("Compensaciones"), XmlArrayItem("Compensacion")> Public Property Compensaciones As List(Of Compensacion)
    <XmlArray("BonoEPCTVs"), XmlArrayItem("BonoEPCTV")> Public Property BonoEPCTVs As List(Of BonoEPCTV)
    <XmlArray("Comisiones"), XmlArrayItem("Comision")> Public Property Comisiones As List(Of Decimal)
    <XmlArray("PagosTerceros"), XmlArrayItem("PagoTercero")> Public Property PagosTerceros As List(Of Decimal)
    <XmlArray("Anticipos"), XmlArrayItem("Anticipo")> Public Property Anticipos As List(Of Decimal)
    <XmlElement("Dotacion")> Public Property Dotacion As Decimal
    <XmlElement("ApoyoSost")> Public Property ApoyoSost As Decimal
    <XmlElement("Teletrabajo")> Public Property Teletrabajo As Decimal
    <XmlElement("BonifRetiro")> Public Property BonifRetiro As Decimal
    <XmlElement("Indemnizacion")> Public Property Indemnizacion As Decimal
    <XmlElement("Reintegro")> Public Property Reintegro As Decimal
End Class

Public Class Basico
    <XmlAttribute("DiasTrabajados")> Public Property DiasTrabajados As String
    <XmlAttribute("SueldoTrabajado")> Public Property SueldoTrabajado As String
End Class

Public Class Transporte
    <XmlAttribute("AuxilioTransporte")> Public Property AuxilioTransporte As String
    <XmlAttribute("ViaticoManuAlojS")> Public Property ViaticoManuAlojS As String
    <XmlAttribute("ViaticoManuAlojNS")> Public Property ViaticoManuAlojNS As String
End Class

Public Class HED
    <XmlAttribute("HoraInicio")> Public Property HoraInicio As String
    <XmlAttribute("HoraFin")> Public Property HoraFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("Pago")> Public Property Pago As String
End Class

Public Class HEN : Inherits HED
End Class
Public Class HRN : Inherits HED
End Class
Public Class HEDDF : Inherits HED
End Class

Public Class HRDDF : Inherits HED
End Class

Public Class HENDF : Inherits HED
End Class

Public Class HRNDF : Inherits HED
End Class

Public Class Vacaciones
    <XmlElement("VacacionesComunes")> Public Property VacacionesComunes As VacacionesComunes
    <XmlElement("VacacionesCompensadas")> Public Property VacacionesCompensadas As VacacionesCompensadas
End Class

Public Class VacacionesComunes
    <XmlAttribute("FechaInicio")> Public Property FechaInicio As String
    <XmlAttribute("FechaFin")> Public Property FechaFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Pago")> Public Property Pago As String
End Class

Public Class VacacionesCompensadas
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Pago")> Public Property Pago As String
End Class

Public Class Primas
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Pago")> Public Property Pago As String
    <XmlAttribute("PagoNS")> Public Property PagoNS As String
End Class

Public Class Cesantias
    <XmlAttribute("Pago")> Public Property Pago As String
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("PagoIntereses")> Public Property PagoIntereses As String
End Class

Public Class Incapacidad
    <XmlAttribute("FechaInicio")> Public Property FechaInicio As String
    <XmlAttribute("FechaFin")> Public Property FechaFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Tipo")> Public Property Tipo As String
    <XmlAttribute("Pago")> Public Property Pago As String
End Class

Public Class Licencias
    <XmlElement("LicenciaMP")> Public Property LicenciaMP As LicenciaMP
    <XmlElement("LicenciaR")> Public Property LicenciaR As LicenciaR
    <XmlElement("LicenciaNR")> Public Property LicenciaNR As LicenciaNR
End Class

Public Class LicenciaMP
    <XmlAttribute("FechaInicio")> Public Property FechaInicio As String
    <XmlAttribute("FechaFin")> Public Property FechaFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
    <XmlAttribute("Pago")> Public Property Pago As String
End Class

Public Class LicenciaR : Inherits LicenciaMP
End Class

Public Class LicenciaNR
    <XmlAttribute("FechaInicio")> Public Property FechaInicio As String
    <XmlAttribute("FechaFin")> Public Property FechaFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
End Class

Public Class Bonificacion
    <XmlAttribute("BonificacionS")> Public Property BonificacionS As String
    <XmlAttribute("BonificacionNS")> Public Property BonificacionNS As String
End Class

Public Class Auxilio
    <XmlAttribute("AuxilioS")> Public Property AuxilioS As String
    <XmlAttribute("AuxilioNS")> Public Property AuxilioNS As String
End Class

Public Class HuelgaLegal
    <XmlAttribute("FechaInicio")> Public Property FechaInicio As String
    <XmlAttribute("FechaFin")> Public Property FechaFin As String
    <XmlAttribute("Cantidad")> Public Property Cantidad As String
End Class

Public Class OtroConcepto
    <XmlAttribute("DescripcionConcepto")> Public Property DescripcionConcepto As String
    <XmlAttribute("ConceptoS")> Public Property ConceptoS As String
    <XmlAttribute("ConceptoNS")> Public Property ConceptoNS As String
End Class

Public Class Compensacion
    <XmlAttribute("CompensacionO")> Public Property CompensacionO As String
    <XmlAttribute("CompensacionE")> Public Property CompensacionE As String
End Class

Public Class BonoEPCTV
    <XmlAttribute("PagoS")> Public Property PagoS As String
    <XmlAttribute("PagoNS")> Public Property PagoNS As String
    <XmlAttribute("PagoAlimentacionS")> Public Property PagoAlimentacionS As String
    <XmlAttribute("PagoAlimentacionNS")> Public Property PagoAlimentacionNS As String
End Class

Public Class Deducciones
    <XmlElement("Salud")> Public Property Salud As Salud
    <XmlElement("FondoPension")> Public Property FondoPension As FondoPension
    <XmlElement("FondoSP")> Public Property FondoSP As FondoSP
    <XmlArray("Sindicatos"), XmlArrayItem("Sindicato")> Public Property Sindicatos As List(Of Sindicato)
    <XmlArray("Sanciones"), XmlArrayItem("Sancion")> Public Property Sanciones As List(Of Sancion)
    <XmlArray("Libranzas"), XmlArrayItem("Libranza")> Public Property Libranzas As List(Of Libranza)
    <XmlArray("PagosTerceros"), XmlArrayItem("PagoTercero")> Public Property PagosTerceros As List(Of Decimal)
    <XmlArray("Anticipos"), XmlArrayItem("Anticipo")> Public Property Anticipos As List(Of Decimal)
    <XmlArray("OtrasDeducciones"), XmlArrayItem("OtraDeduccion")> Public Property OtrasDeducciones As List(Of Decimal)
    <XmlElement("PensionVoluntaria")> Public Property PensionVoluntaria As Decimal
    <XmlElement("RetencionFuente")> Public Property RetencionFuente As Decimal
    <XmlElement("AFC")> Public Property AFC As Decimal
    <XmlElement("Cooperativa")> Public Property Cooperativa As Decimal
    <XmlElement("EmbargoFiscal")> Public Property EmbargoFiscal As Decimal
    <XmlElement("PlanComplementarios")> Public Property PlanComplementarios As Decimal
    <XmlElement("Educacion")> Public Property Educacion As Decimal
    <XmlElement("Reintegro")> Public Property Reintegro As Decimal
    <XmlElement("Deuda")> Public Property Deuda As Decimal
End Class

Public Class Salud
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("Deduccion")> Public Property Deduccion As String
End Class

Public Class FondoPension
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("Deduccion")> Public Property Deduccion As String
End Class

Public Class FondoSP
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("DeduccionSP")> Public Property DeduccionSP As String
    <XmlAttribute("PorcentajeSub")> Public Property PorcentajeSub As String
    <XmlAttribute("DeduccionSub")> Public Property DeduccionSub As String
End Class

Public Class Sindicato
    <XmlAttribute("Porcentaje")> Public Property Porcentaje As String
    <XmlAttribute("Deduccion")> Public Property Deduccion As String
End Class

Public Class Sancion
    <XmlAttribute("SancionPublic")> Public Property SancionPublic As String
    <XmlAttribute("SancionPriv")> Public Property SancionPriv As String
End Class

Public Class Libranza
    <XmlAttribute("Descripcion")> Public Property Descripcion As String
    <XmlAttribute("Deduccion")> Public Property Deduccion As String
End Class
