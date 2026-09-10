'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PContract

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContract

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
    Public Sub New(ByRef iview As IContract)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeHealtAdministrator()
        Using model As New MBusqueda
            View.HealthAdministratorXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthAdministratorByStatus, True)
        End Using
    End Sub

    Public Sub InitializeContractEntity()
        Using model As New MBusqueda
            View.ContractEntityXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListContractEntityByStatus, True)
        End Using
    End Sub

    Public Function InitializeAges(OperatingUnitId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListAgesPortfolioByOperatingUnitId(OperatingUnitId)
    End Function

    Public Function InitializePolicy() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPolizaByStatus(True)
    End Function

    Public Function InitializeInsurance() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetInsuranceByStatus(True)
    End Function

    Public Function GetFixedAssetPolicyById(Id As Integer) As Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPolizaXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPolizaXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetContractDetailsByContractId(ContractId As Integer) As List(Of ContractDetailXpo)
        Dim filter As String = "ContractId.Id = " & ContractId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractDetailXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
