'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class PCupsSubGroup

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ICupsSubGroup

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICupsSubGroup)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub InitializeCupsGroup()
        Using model As New MBusqueda
            View.CupsGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsGroupByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el grupo cups por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCupsGroupById(Id As Integer) As CupsGroupXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of CupsGroupXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta grupos de imagenologia
    ''' </summary>
    Public Async Sub GetImagingGroupActive()
        'View.ImagingGroups = (Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetImagingGroupActiveAsync()).ObjectEmbbeded
        If View.ImagingGroups Is Nothing Then
            View.ImagingGroups = XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.ListImagingGroupActive()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el grupo de imagenologia
    ''' </summary>
    Public Function GetImagingGroupById(Id As Integer) As RISGRIMAGEXpo
        Dim filter As String = "ID = " & Id
        Return XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.GetCollection(Of RISGRIMAGEXpo)(Nothing, Filter).FirstOrDefault()
    End Function

    '''' <summary>
    '''' Consulta un grupo de imagenologia por id
    '''' </summary>
    'Public Async Sub GetImagingGroupById(id As Integer)
    '    View.ImagingGroup = (Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetImagingGroupByIdAsync(id, Me.Indigo.AuditMessageWcf)).ObjectEmbbeded
    'End Sub

#End Region

End Class
