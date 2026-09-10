'***********************************************************************
' Assembly         : Presentation.Billing.MVP
' Author           : Andres Alarcon
' Created          : 21-11-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Presentador para Registro Soporte RIPS
'
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
#End Region

''' <summary>
''' Presentador para el formulario de Registro Soporte RIPS.
''' Actúa como intermediario entre las vistas y el modelo, coordinando
''' la carga de datos y las acciones del usuario.
''' </summary>
''' <remarks>
''' Este presentador soporta dos vistas:
''' - IRIPSSupportRecord: Vista principal del formulario RIPS
''' - IRIPSSupportRecordDetailsView: Vista del popup de detalles de estancia
''' </remarks>
Public Class PRIPSSupportRecord

#Region "Campos Privados"

    ''' <summary>Vista del popup de detalles de estancia hospitalaria.</summary>
    Private ReadOnly _detailView As IRIPSSupportRecordDetailsView

    ''' <summary>Modelo de datos para acceso a servicios.</summary>
    Private ReadOnly _model As MRIPSSupportRecord

    ''' <summary>Vista principal del formulario RIPS.</summary>
    Private ReadOnly _view As IRIPSSupportRecord

#End Region

#Region "Constructores"

    ''' <summary>
    ''' Inicializa el presentador para la vista de detalles (popup).
    ''' </summary>
    ''' <param name="detailView">Instancia de la vista de detalles.</param>
    ''' <param name="tag">Identificador del formulario para auditoría.</param>
    Public Sub New(detailView As IRIPSSupportRecordDetailsView, tag As String)
        _detailView = detailView
        _model = New MRIPSSupportRecord(tag)
    End Sub

    ''' <summary>
    ''' Inicializa el presentador para la vista principal del formulario.
    ''' </summary>
    ''' <param name="view">Instancia de la vista principal.</param>
    ''' <param name="tag">Identificador del formulario para auditoría.</param>
    Public Sub New(view As IRIPSSupportRecord, tag As String)
        _view = view
        _model = New MRIPSSupportRecord(tag)
    End Sub

#End Region

#Region "Métodos para Vista de Detalles"

    ''' <summary>
    ''' Carga la información inicial de combos y listas del formulario de detalles.
    ''' Incluye: unidades funcionales, diagnósticos, profesionales y especialidades.
    ''' </summary>
    Public Sub LoadInitialData()
        _detailView.FunctionalUnitsDataSource = _model.GetHospitalFunctionalUnits()

        ' Los diagnósticos activos son los mismos para Principal, Relacionado y Causa de Muerte
        _detailView.PrincipalDiagnosisDataSource = _model.GetPrincipalDiagnoses()
        _detailView.RelatedDiagnosisDataSource = _model.GetPrincipalDiagnoses()
        _detailView.DeathCauseDiagnosisDataSource = _model.GetPrincipalDiagnoses()

        _detailView.HealthProfessionalsDataSource = _model.GetHealthProfessionals()
        _detailView.SpecialtiesDataSource = _model.GetSpecialties()
    End Sub

    ''' <summary>
    ''' Maneja el cambio de unidad funcional cargando los tipos de estancia asociados.
    ''' </summary>
    ''' <param name="functionalUnitCode">Código de la unidad funcional seleccionada.</param>
    Public Sub OnFunctionalUnitChanged(functionalUnitCode As String)
        _detailView.StayTypesDataSource = _model.GetStayTypesByFunctionalUnit(functionalUnitCode)
    End Sub

#End Region

#Region "Métodos para Vista Principal"

    ''' <summary>
    ''' Obtiene la secuencia numérica configurada para el formulario y la asigna a la vista.
    ''' </summary>
    ''' <remarks>
    ''' La secuencia determina cómo se generan los códigos de registro:
    ''' - Manual: el usuario digita el código
    ''' - Automática: el sistema genera el código según el patrón configurado
    ''' </remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(_view.MyTag)
            _view.Sequence = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
