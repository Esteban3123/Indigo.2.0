'***********************************************************************
' Assembly         : Presentation.Maintenance
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 04-02-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent


#End Region

Public Class PMaintenanceFailureRequest

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IMaintenanceFailureRequest

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues


#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IMaintenanceFailureRequest)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al funcional
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenseMaintenance(Me.View.MyTag)
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetByTransactionClass(TransactionClass As Byte, PhysicalAssetId As Integer?, PhysicalAssetPartsId As Integer?) As FixedAssetPhysicalAssetXpo
        If TransactionClass = 1 Then
            Dim filter As String = "Id = " & PhysicalAssetId
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filter).FirstOrDefault()
        Else
            Dim filter As String = "Id = " & PhysicalAssetPartsId
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetPhysicalAssetPartsXpo)(Nothing, filter).FirstOrDefault().PhysicalAssetId
        End If
    End Function

    Public Function LoadBranchOffice() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListBranchOfficeByState(True)
    End Function

#End Region

End Class
