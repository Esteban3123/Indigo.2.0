Imports System.Runtime.Serialization

Public Class SP_GenerateFURIPS1FileData_Result

    Public Property NumeroRadicadoAnterior() As String
        Get
            Return _numeroRadicadoAnterior
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroRadicadoAnterior, value) Then
                _numeroRadicadoAnterior = value
            End If
        End Set
    End Property

    Private _numeroRadicadoAnterior As String

    <DataMember()>
    Public Property RespuestaGlosa() As String
        Get
            Return _respuestaGlosa
        End Get
        Set(ByVal value As String)
            If Not Equals(_respuestaGlosa, value) Then
                _respuestaGlosa = value
            End If
        End Set
    End Property

    Private _respuestaGlosa As String

    <DataMember()>
    Public Property NumeroFactura() As String
        Get
            Return _numeroFactura
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroFactura, value) Then
                _numeroFactura = value
            End If
        End Set
    End Property

    Private _numeroFactura As String

    <DataMember()>
    Public Property ConsecutivoReclamacion() As String
        Get
            Return _consecutivoReclamacion
        End Get
        Set(ByVal value As String)
            If Not Equals(_consecutivoReclamacion, value) Then
                _consecutivoReclamacion = value
            End If
        End Set
    End Property

    Private _consecutivoReclamacion As String

    <DataMember()>
    Public Property PrestadorServicioSalud() As String
        Get
            Return _prestadorServicioSalud
        End Get
        Set(ByVal value As String)
            If Not Equals(_prestadorServicioSalud, value) Then
                _prestadorServicioSalud = value
            End If
        End Set
    End Property

    Private _prestadorServicioSalud As String

    <DataMember()>
    Public Property PrimerApellidoVictima() As String
        Get
            Return _primerApellidoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerApellidoVictima, value) Then
                _primerApellidoVictima = value
            End If
        End Set
    End Property

    Private _primerApellidoVictima As String

    <DataMember()>
    Public Property SegundoApellidoVictima() As String
        Get
            Return _segundoApellidoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoApellidoVictima, value) Then
                _segundoApellidoVictima = value
            End If
        End Set
    End Property

    Private _segundoApellidoVictima As String

    <DataMember()>
    Public Property PrimerNombreVictima() As String
        Get
            Return _primerNombreVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerNombreVictima, value) Then
                _primerNombreVictima = value
            End If
        End Set
    End Property

    Private _primerNombreVictima As String

    <DataMember()>
    Public Property SegundoNombreVictima() As String
        Get
            Return _segundoNombreVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoNombreVictima, value) Then
                _segundoNombreVictima = value
            End If
        End Set
    End Property

    Private _segundoNombreVictima As String

    <DataMember()>
    Public Property TipoDocumentoVictima() As String
        Get
            Return _tipoDocumentoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoDocumentoVictima, value) Then
                _tipoDocumentoVictima = value
            End If
        End Set
    End Property

    Private _tipoDocumentoVictima As String

    <DataMember()>
    Public Property NumeroDocumentoVictima() As String
        Get
            Return _numeroDocumentoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroDocumentoVictima, value) Then
                _numeroDocumentoVictima = value
            End If
        End Set
    End Property

    Private _numeroDocumentoVictima As String

    <DataMember()>
    Public Property FechaNacimientoVictima() As String
        Get
            Return _fechaNacimientoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaNacimientoVictima, value) Then
                _fechaNacimientoVictima = value
            End If
        End Set
    End Property

    Private _fechaNacimientoVictima As String

    <DataMember()>
    Public Property FechaFallecimientoVictima() As String
        Get
            Return _fechaFallecimientoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaFallecimientoVictima, value) Then
                _fechaFallecimientoVictima = value
            End If
        End Set
    End Property

    Private _fechaFallecimientoVictima As String

    <DataMember()>
    Public Property SexoVictima() As String
        Get
            Return _sexoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_sexoVictima, value) Then
                _sexoVictima = value
            End If
        End Set
    End Property

    Private _sexoVictima As String

    <DataMember()>
    Public Property DireccionVictima() As String
        Get
            Return _direccionVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_direccionVictima, value) Then
                _direccionVictima = value
            End If
        End Set
    End Property

    Private _direccionVictima As String

    <DataMember()>
    Public Property DepartamentoResidenciaVictima() As String
        Get
            Return _departamentoResidenciaVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_departamentoResidenciaVictima, value) Then
                _departamentoResidenciaVictima = value
            End If
        End Set
    End Property

    Private _departamentoResidenciaVictima As String

    <DataMember()>
    Public Property CodigoMunicipioVictima() As String
        Get
            Return _codigoMunicipioVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoMunicipioVictima, value) Then
                _codigoMunicipioVictima = value
            End If
        End Set
    End Property

    Private _codigoMunicipioVictima As String

    <DataMember()>
    Public Property TelefonoVictima() As String
        Get
            Return _telefonoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_telefonoVictima, value) Then
                _telefonoVictima = value
            End If
        End Set
    End Property

    Private _telefonoVictima As String

    <DataMember()>
    Public Property CondicionVictima() As String
        Get
            Return _condicionVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_condicionVictima, value) Then
                _condicionVictima = value
            End If
        End Set
    End Property

    Private _condicionVictima As String

    <DataMember()>
    Public Property NaturalezaEvento() As String
        Get
            Return _naturalezaEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_naturalezaEvento, value) Then
                _naturalezaEvento = value
            End If
        End Set
    End Property

    Private _naturalezaEvento As String

    <DataMember()>
    Public Property DescripcionOtherEvento() As String
        Get
            Return _DescripcionOtherEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_DescripcionOtherEvento, value) Then
                _DescripcionOtherEvento = value
            End If
        End Set
    End Property

    Private _DescripcionOtherEvento As String

    <DataMember()>
    Public Property DireccionEvento() As String
        Get
            Return _direccionEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_direccionEvento, value) Then
                _direccionEvento = value
            End If
        End Set
    End Property

    Private _direccionEvento As String

    <DataMember()>
    Public Property FechaEvento() As String
        Get
            Return _fechaEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaEvento, value) Then
                _fechaEvento = value
            End If
        End Set
    End Property

    Private _fechaEvento As String

    <DataMember()>
    Public Property HoraEvento() As String
        Get
            Return _horaEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_horaEvento, value) Then
                _horaEvento = value
            End If
        End Set
    End Property

    Private _horaEvento As String

    <DataMember()>
    Public Property DepartamentoEvento() As String
        Get
            Return _departamentoEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_departamentoEvento, value) Then
                _departamentoEvento = value
            End If
        End Set
    End Property

    Private _departamentoEvento As String

    <DataMember()>
    Public Property CiudadEvento() As String
        Get
            Return _ciudadEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_ciudadEvento, value) Then
                _ciudadEvento = value
            End If
        End Set
    End Property

    Private _ciudadEvento As String

    <DataMember()>
    Public Property ZonaEvento() As String
        Get
            Return _zonaEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_zonaEvento, value) Then
                _zonaEvento = value
            End If
        End Set
    End Property

    Private _zonaEvento As String

    <DataMember()>
    Public Property EstadoAsegurado() As String
        Get
            Return _estadoAsegurado
        End Get
        Set(ByVal value As String)
            If Not Equals(_estadoAsegurado, value) Then
                _estadoAsegurado = value
            End If
        End Set
    End Property

    Private _estadoAsegurado As String

    <DataMember()>
    Public Property Marca() As String
        Get
            Return _marca
        End Get
        Set(ByVal value As String)
            If Not Equals(_marca, value) Then
                _marca = value
            End If
        End Set
    End Property

    Private _marca As String

    <DataMember()>
    Public Property Placa() As String
        Get
            Return _placa
        End Get
        Set(ByVal value As String)
            If Not Equals(_placa, value) Then
                _placa = value
            End If
        End Set
    End Property

    Private _placa As String

    <DataMember()>
    Public Property TipoVehiculo() As String
        Get
            Return _tipoVehiculo
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoVehiculo, value) Then
                _tipoVehiculo = value
            End If
        End Set
    End Property

    Private _tipoVehiculo As String

    <DataMember()>
    Public Property CodigoAseguradora() As String
        Get
            Return _codigoAseguradora
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoAseguradora, value) Then
                _codigoAseguradora = value
            End If
        End Set
    End Property

    Private _codigoAseguradora As String

    <DataMember()>
    Public Property NumeroSOAT() As String
        Get
            Return _numeroSOAT
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroSOAT, value) Then
                _numeroSOAT = value
            End If
        End Set
    End Property

    Private _numeroSOAT As String

    <DataMember()>
    Public Property FechaInicioVigenciaPoliza() As String
        Get
            Return _fechaInicioVigenciaPoliza
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaInicioVigenciaPoliza, value) Then
                _fechaInicioVigenciaPoliza = value
            End If
        End Set
    End Property

    Private _fechaInicioVigenciaPoliza As String

    <DataMember()>
    Public Property FechaFinVigenciaPoliza() As String
        Get
            Return _fechaFinVigenciaPoliza
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaFinVigenciaPoliza, value) Then
                _fechaFinVigenciaPoliza = value
            End If
        End Set
    End Property

    Private _fechaFinVigenciaPoliza As String

    <DataMember()>
    Public Property NumeroRadicadoSIRAS() As String
        Get
            Return _numeroRadicadoSIRAS
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroRadicadoSIRAS, value) Then
                _numeroRadicadoSIRAS = value
            End If
        End Set
    End Property

    Private _numeroRadicadoSIRAS As String

    <DataMember()>
    Public Property IntervencionAutoridad() As String
        Get
            Return _intervencionAutoridad
        End Get
        Set(ByVal value As String)
            If Not Equals(_intervencionAutoridad, value) Then
                _intervencionAutoridad = value
            End If
        End Set
    End Property

    Private _intervencionAutoridad As String

    <DataMember()>
    Public Property CobroExcedente() As String
        Get
            Return _cobroExcedente
        End Get
        Set(ByVal value As String)
            If Not Equals(_cobroExcedente, value) Then
                _cobroExcedente = value
            End If
        End Set
    End Property

    Private _cobroExcedente As String

    <DataMember()>
    Public Property CodigoCupsServicio() As String
        Get
            Return _codigoCupsServicio
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoCupsServicio, value) Then
                _codigoCupsServicio = value
            End If
        End Set
    End Property

    Private _codigoCupsServicio As String

    <DataMember()>
    Public Property ComplejidadProcedimientoQuirurgico() As String
        Get
            Return _complejidadProcedimientoQuirurgico
        End Get
        Set(ByVal value As String)
            If Not Equals(_complejidadProcedimientoQuirurgico, value) Then
                _complejidadProcedimientoQuirurgico = value
            End If
        End Set
    End Property

    Private _complejidadProcedimientoQuirurgico As String

    <DataMember()>
    Public Property CodigoCupsProcedimientoQuirurgicoPrincipal() As String
        Get
            Return _codigoCupsProcedimientoQuirurgicoPrincipal
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoCupsProcedimientoQuirurgicoPrincipal, value) Then
                _codigoCupsProcedimientoQuirurgicoPrincipal = value
            End If
        End Set
    End Property

    Private _codigoCupsProcedimientoQuirurgicoPrincipal As String

    <DataMember()>
    Public Property CodigoCupsProcedimientoQuirurgicoSecundario() As String
        Get
            Return _codigoCupsProcedimientoQuirurgicoSecundario
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoCupsProcedimientoQuirurgicoSecundario, value) Then
                _codigoCupsProcedimientoQuirurgicoSecundario = value
            End If
        End Set
    End Property

    Private _codigoCupsProcedimientoQuirurgicoSecundario As String

    <DataMember()>
    Public Property SePrestoServicioUCI() As String
        Get
            Return _sePrestoServicioUCI
        End Get
        Set(ByVal value As String)
            If Not Equals(_sePrestoServicioUCI, value) Then
                _sePrestoServicioUCI = value
            End If
        End Set
    End Property

    Private _sePrestoServicioUCI As String

    <DataMember()>
    Public Property DiasDeUciReclamados() As String
        Get
            Return _diasDeUciReclamados
        End Get
        Set(ByVal value As String)
            If Not Equals(_diasDeUciReclamados, value) Then
                _diasDeUciReclamados = value
            End If
        End Set
    End Property

    Private _diasDeUciReclamados As String

    <DataMember()>
    Public Property TipoDocumentoPropietario() As String
        Get
            Return _tipoDocumentoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoDocumentoPropietario, value) Then
                _tipoDocumentoPropietario = value
            End If
        End Set
    End Property

    Private _tipoDocumentoPropietario As String

    <DataMember()>
    Public Property NumeroDocumentoPropietario() As String
        Get
            Return _numeroDocumentoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroDocumentoPropietario, value) Then
                _numeroDocumentoPropietario = value
            End If
        End Set
    End Property

    Private _numeroDocumentoPropietario As String

    <DataMember()>
    Public Property PrimerApellidoPropietario() As String
        Get
            Return _primerApellidoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerApellidoPropietario, value) Then
                _primerApellidoPropietario = value
            End If
        End Set
    End Property

    Private _primerApellidoPropietario As String

    <DataMember()>
    Public Property SegundoApellidoPropietario() As String
        Get
            Return _segundoApellidoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoApellidoPropietario, value) Then
                _segundoApellidoPropietario = value
            End If
        End Set
    End Property

    Private _segundoApellidoPropietario As String

    <DataMember()>
    Public Property PrimerNombrePropietario() As String
        Get
            Return _primerNombrePropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerNombrePropietario, value) Then
                _primerNombrePropietario = value
            End If
        End Set
    End Property

    Private _primerNombrePropietario As String

    <DataMember()>
    Public Property SegundoNombrePropietario() As String
        Get
            Return _segundoNombrePropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoNombrePropietario, value) Then
                _segundoNombrePropietario = value
            End If
        End Set
    End Property

    Private _segundoNombrePropietario As String

    <DataMember()>
    Public Property DireccionPropietario() As String
        Get
            Return _direccionPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_direccionPropietario, value) Then
                _direccionPropietario = value
            End If
        End Set
    End Property

    Private _direccionPropietario As String

    <DataMember()>
    Public Property TelefonoPropietario() As String
        Get
            Return _telefonoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_telefonoPropietario, value) Then
                _telefonoPropietario = value
            End If
        End Set
    End Property

    Private _telefonoPropietario As String

    <DataMember()>
    Public Property DepartamentoPropietario() As String
        Get
            Return _departamentoPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_departamentoPropietario, value) Then
                _departamentoPropietario = value
            End If
        End Set
    End Property

    Private _departamentoPropietario As String

    <DataMember()>
    Public Property MunicipioPropietario() As String
        Get
            Return _municipioPropietario
        End Get
        Set(ByVal value As String)
            If Not Equals(_municipioPropietario, value) Then
                _municipioPropietario = value
            End If
        End Set
    End Property

    Private _municipioPropietario As String

    <DataMember()>
    Public Property PrimerApellidoConductor() As String
        Get
            Return _primerApellidoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerApellidoConductor, value) Then
                _primerApellidoConductor = value
            End If
        End Set
    End Property

    Private _primerApellidoConductor As String

    <DataMember()>
    Public Property SegundoApellidoConductor() As String
        Get
            Return _segundoApellidoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoApellidoConductor, value) Then
                _segundoApellidoConductor = value
            End If
        End Set
    End Property

    Private _segundoApellidoConductor As String

    <DataMember()>
    Public Property PrimerNombreConductor() As String
        Get
            Return _primerNombreConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerNombreConductor, value) Then
                _primerNombreConductor = value
            End If
        End Set
    End Property

    Private _primerNombreConductor As String

    <DataMember()>
    Public Property SegundoNombreConductor() As String
        Get
            Return _segundoNombreConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoNombreConductor, value) Then
                _segundoNombreConductor = value
            End If
        End Set
    End Property

    Private _segundoNombreConductor As String

    <DataMember()>
    Public Property TipoDocumentoConductor() As String
        Get
            Return _tipoDocumentoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoDocumentoConductor, value) Then
                _tipoDocumentoConductor = value
            End If
        End Set
    End Property

    Private _tipoDocumentoConductor As String

    <DataMember()>
    Public Property NumeroDocumentoConductor() As String
        Get
            Return _numeroDocumentoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroDocumentoConductor, value) Then
                _numeroDocumentoConductor = value
            End If
        End Set
    End Property

    Private _numeroDocumentoConductor As String

    <DataMember()>
    Public Property DireccionConductor() As String
        Get
            Return _direccionConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_direccionConductor, value) Then
                _direccionConductor = value
            End If
        End Set
    End Property

    Private _direccionConductor As String

    <DataMember()>
    Public Property DepartamentoConductor() As String
        Get
            Return _departamentoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_departamentoConductor, value) Then
                _departamentoConductor = value
            End If
        End Set
    End Property

    Private _departamentoConductor As String

    <DataMember()>
    Public Property MunicipioConductor() As String
        Get
            Return _municipioConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_municipioConductor, value) Then
                _municipioConductor = value
            End If
        End Set
    End Property

    Private _municipioConductor As String

    <DataMember()>
    Public Property TelefonoConductor() As String
        Get
            Return _telefonoConductor
        End Get
        Set(ByVal value As String)
            If Not Equals(_telefonoConductor, value) Then
                _telefonoConductor = value
            End If
        End Set
    End Property

    Private _telefonoConductor As String

    <DataMember()>
    Public Property TipoReferencia() As String
        Get
            Return _tipoReferencia
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoReferencia, value) Then
                _tipoReferencia = value
            End If
        End Set
    End Property

    Private _tipoReferencia As String

    <DataMember()>
    Public Property FechaRemison() As String
        Get
            Return _fechaRemison
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaRemison, value) Then
                _fechaRemison = value
            End If
        End Set
    End Property

    Private _fechaRemison As String

    <DataMember()>
    Public Property HoraSalida() As String
        Get
            Return _horaSalida
        End Get
        Set(ByVal value As String)
            If Not Equals(_horaSalida, value) Then
                _horaSalida = value
            End If
        End Set
    End Property

    Private _horaSalida As String

    <DataMember()>
    Public Property CodigoHabilitacionPrestador() As String
        Get
            Return _codigoHabilitacionPrestador
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoHabilitacionPrestador, value) Then
                _codigoHabilitacionPrestador = value
            End If
        End Set
    End Property

    Private _codigoHabilitacionPrestador As String

    <DataMember()>
    Public Property ProfesionalRemite() As String
        Get
            Return _profesionalRemite
        End Get
        Set(ByVal value As String)
            If Not Equals(_profesionalRemite, value) Then
                _profesionalRemite = value
            End If
        End Set
    End Property

    Private _profesionalRemite As String

    <DataMember()>
    Public Property CargoProfesionalRemite() As String
        Get
            Return _cargoProfesionalRemite
        End Get
        Set(ByVal value As String)
            If Not Equals(_cargoProfesionalRemite, value) Then
                _cargoProfesionalRemite = value
            End If
        End Set
    End Property

    Private _cargoProfesionalRemite As String

    <DataMember()>
    Public Property FechaIngreso() As String
        Get
            Return _fechaIngreso
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaIngreso, value) Then
                _fechaIngreso = value
            End If
        End Set
    End Property

    Private _fechaIngreso As String

    <DataMember()>
    Public Property HoraIngreso() As String
        Get
            Return _horaIngreso
        End Get
        Set(ByVal value As String)
            If Not Equals(_horaIngreso, value) Then
                _horaIngreso = value
            End If
        End Set
    End Property

    Private _horaIngreso As String

    <DataMember()>
    Public Property CodigoHabilitacionRecibe() As String
        Get
            Return _codigoHabilitacionRecibe
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoHabilitacionRecibe, value) Then
                _codigoHabilitacionRecibe = value
            End If
        End Set
    End Property

    Private _codigoHabilitacionRecibe As String

    <DataMember()>
    Public Property ProfesionalRecibe() As String
        Get
            Return _profesionalRecibe
        End Get
        Set(ByVal value As String)
            If Not Equals(_profesionalRecibe, value) Then
                _profesionalRecibe = value
            End If
        End Set
    End Property

    Private _profesionalRecibe As String

    <DataMember()>
    Public Property CargoProfesionalRecibe() As String
        Get
            Return _cargoProfesionalRecibe
        End Get
        Set(ByVal value As String)
            If Not Equals(_cargoProfesionalRecibe, value) Then
                _cargoProfesionalRecibe = value
            End If
        End Set
    End Property

    Private _cargoProfesionalRecibe As String

    <DataMember()>
    Public Property PlacaTrasladoInterinstitucional() As String
        Get
            Return _placaTrasladoInterinstitucional
        End Get
        Set(ByVal value As String)
            If Not Equals(_placaTrasladoInterinstitucional, value) Then
                _placaTrasladoInterinstitucional = value
            End If
        End Set
    End Property

    Private _placaTrasladoInterinstitucional As String

    <DataMember()>
    Public Property PlacaAmbulancia() As String
        Get
            Return _placaAmbulancia
        End Get
        Set(ByVal value As String)
            If Not Equals(_placaAmbulancia, value) Then
                _placaAmbulancia = value
            End If
        End Set
    End Property

    Private _placaAmbulancia As String

    <DataMember()>
    Public Property TransporteDesde() As String
        Get
            Return _transporteDesde
        End Get
        Set(ByVal value As String)
            If Not Equals(_transporteDesde, value) Then
                _transporteDesde = value
            End If
        End Set
    End Property

    Private _transporteDesde As String

    <DataMember()>
    Public Property TransporteHasta() As String
        Get
            Return _transporteHasta
        End Get
        Set(ByVal value As String)
            If Not Equals(_transporteHasta, value) Then
                _transporteHasta = value
            End If
        End Set
    End Property

    Private _transporteHasta As String

    <DataMember()>
    Public Property TipoServicioAmbulancia() As String
        Get
            Return _tipoServicioAmbulancia
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoServicioAmbulancia, value) Then
                _tipoServicioAmbulancia = value
            End If
        End Set
    End Property

    Private _tipoServicioAmbulancia As String

    <DataMember()>
    Public Property ZonaRecogeAmbulancia() As String
        Get
            Return _zonaRecogeAmbulancia
        End Get
        Set(ByVal value As String)
            If Not Equals(_zonaRecogeAmbulancia, value) Then
                _zonaRecogeAmbulancia = value
            End If
        End Set
    End Property

    Private _zonaRecogeAmbulancia As String

    <DataMember()>
    Public Property FechaIngresoVictima() As String
        Get
            Return _fechaIngresoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaIngresoVictima, value) Then
                _fechaIngresoVictima = value
            End If
        End Set
    End Property

    Private _fechaIngresoVictima As String

    <DataMember()>
    Public Property HoraIngresoVictima() As String
        Get
            Return _horaIngresoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_horaIngresoVictima, value) Then
                _horaIngresoVictima = value
            End If
        End Set
    End Property

    Private _horaIngresoVictima As String

    <DataMember()>
    Public Property FechaEgresoVictima() As String
        Get
            Return _fechaEgresoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_fechaEgresoVictima, value) Then
                _fechaEgresoVictima = value
            End If
        End Set
    End Property

    Private _fechaEgresoVictima As String

    <DataMember()>
    Public Property HoraEgresoVictima() As String
        Get
            Return _horaEgresoVictima
        End Get
        Set(ByVal value As String)
            If Not Equals(_horaEgresoVictima, value) Then
                _horaEgresoVictima = value
            End If
        End Set
    End Property

    Private _horaEgresoVictima As String

    <DataMember()>
    Public Property CodigoDiagnosticoIngresoPrincipal() As String
        Get
            Return _codigoDiagnosticoIngresoPrincipal
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoIngresoPrincipal, value) Then
                _codigoDiagnosticoIngresoPrincipal = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoIngresoPrincipal As String

    <DataMember()>
    Public Property CodigoDiagnosticoIngresoUno() As String
        Get
            Return _codigoDiagnosticoIngresoUno
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoIngresoUno, value) Then
                _codigoDiagnosticoIngresoUno = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoIngresoUno As String

    <DataMember()>
    Public Property CodigoDiagnosticoIngresoDos() As String
        Get
            Return _codigoDiagnosticoIngresoDos
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoIngresoDos, value) Then
                _codigoDiagnosticoIngresoDos = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoIngresoDos As String

    <DataMember()>
    Public Property CodigoDiagnosticoEgresoPrincipal() As String
        Get
            Return _codigoDiagnosticoEgresoPrincipal
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoEgresoPrincipal, value) Then
                _codigoDiagnosticoEgresoPrincipal = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoEgresoPrincipal As String

    <DataMember()>
    Public Property CodigoDiagnosticoEgresoUno() As String
        Get
            Return _codigoDiagnosticoEgresoUno
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoEgresoUno, value) Then
                _codigoDiagnosticoEgresoUno = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoEgresoUno As String

    <DataMember()>
    Public Property CodigoDiagnosticoEgresoDos() As String
        Get
            Return _codigoDiagnosticoEgresoDos
        End Get
        Set(ByVal value As String)
            If Not Equals(_codigoDiagnosticoEgresoDos, value) Then
                _codigoDiagnosticoEgresoDos = value
            End If
        End Set
    End Property

    Private _codigoDiagnosticoEgresoDos As String

    <DataMember()>
    Public Property PrimerApellidoMedico() As String
        Get
            Return _primerApellidoMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerApellidoMedico, value) Then
                _primerApellidoMedico = value
            End If
        End Set
    End Property

    Private _primerApellidoMedico As String

    <DataMember()>
    Public Property SegundoApellidoMedico() As String
        Get
            Return _segundoApellidoMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoApellidoMedico, value) Then
                _segundoApellidoMedico = value
            End If
        End Set
    End Property

    Private _segundoApellidoMedico As String

    <DataMember()>
    Public Property PrimerNombreMedico() As String
        Get
            Return _primerNombreMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_primerNombreMedico, value) Then
                _primerNombreMedico = value
            End If
        End Set
    End Property

    Private _primerNombreMedico As String

    <DataMember()>
    Public Property SegundoNombreMedico() As String
        Get
            Return _segundoNombreMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_segundoNombreMedico, value) Then
                _segundoNombreMedico = value
            End If
        End Set
    End Property

    Private _segundoNombreMedico As String

    <DataMember()>
    Public Property TipoDocumentoMedico() As String
        Get
            Return _tipoDocumentoMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_tipoDocumentoMedico, value) Then
                _tipoDocumentoMedico = value
            End If
        End Set
    End Property

    Private _tipoDocumentoMedico As String

    <DataMember()>
    Public Property NumeroDocumentoMedico() As String
        Get
            Return _numeroDocumentoMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroDocumentoMedico, value) Then
                _numeroDocumentoMedico = value
            End If
        End Set
    End Property

    Private _numeroDocumentoMedico As String

    <DataMember()>
    Public Property NumeroRegistroMedico() As String
        Get
            Return _numeroRegistroMedico
        End Get
        Set(ByVal value As String)
            If Not Equals(_numeroRegistroMedico, value) Then
                _numeroRegistroMedico = value
            End If
        End Set
    End Property

    Private _numeroRegistroMedico As String

    <DataMember()>
    Public Property TotalFacturadoGastosMedicos() As String
        Get
            Return _totalFacturadoGastosMedicos
        End Get
        Set(ByVal value As String)
            If Not Equals(_totalFacturadoGastosMedicos, value) Then
                _totalFacturadoGastosMedicos = value
            End If
        End Set
    End Property

    Private _totalFacturadoGastosMedicos As String

    <DataMember()>
    Public Property TotalReclamadoGastosMedicos() As String
        Get
            Return _totalReclamadoGastosMedicos
        End Get
        Set(ByVal value As String)
            If Not Equals(_totalReclamadoGastosMedicos, value) Then
                _totalReclamadoGastosMedicos = value
            End If
        End Set
    End Property

    Private _totalReclamadoGastosMedicos As String

    <DataMember()>
    Public Property TotalFacturadoMovilizacion() As String
        Get
            Return _totalFacturadoMovilizacion
        End Get
        Set(ByVal value As String)
            If Not Equals(_totalFacturadoMovilizacion, value) Then
                _totalFacturadoMovilizacion = value
            End If
        End Set
    End Property

    Private _totalFacturadoMovilizacion As String

    <DataMember()>
    Public Property TotalReclamadoMovilizacion() As String
        Get
            Return _totalReclamadoMovilizacion
        End Get
        Set(ByVal value As String)
            If Not Equals(_totalReclamadoMovilizacion, value) Then
                _totalReclamadoMovilizacion = value
            End If
        End Set
    End Property

    Private _totalReclamadoMovilizacion As String

    <DataMember()>
    Public Property TotalFolios() As String
        Get
            Return _totalFolios
        End Get
        Set(ByVal value As String)
            If Not Equals(_totalFolios, value) Then
                _totalFolios = value
            End If
        End Set
    End Property

    Private _totalFolios As String

    <DataMember()>
    Public Property ManifestacionServiciosHabilitados() As String
        Get
            Return _manifestacionServiciosHabilitados
        End Get
        Set(ByVal value As String)
            If Not Equals(_manifestacionServiciosHabilitados, value) Then
                _manifestacionServiciosHabilitados = value
            End If
        End Set
    End Property

    Private _manifestacionServiciosHabilitados As String

    <DataMember()>
    Public Property DescripcionEvento() As String
        Get
            Return _descripcionEvento
        End Get
        Set(ByVal value As String)
            If Not Equals(_descripcionEvento, value) Then
                _descripcionEvento = value
            End If
        End Set
    End Property

    Private _descripcionEvento As String

End Class
