'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Andres Alarcon
' Created          : 18-05-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports Presentation.Payroll.MVP

#End Region

Public Class PProductAndServiceFee

#Region "Variables"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IProductAndServiceFee

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IProductAndServiceFee)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Lista los productos que esten activados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeProducts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProduct(True)
    End Function

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeServices() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAssociatedMainServiceId(0, 0, 1)
    End Function

    Public Sub InitializeUsers()
        Using model As New MFunctionalUnit("")
            Me.View.UserXpo = model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub

    ''' <summary>
    ''' consulta el producto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function ProductbyId(Id As Integer) As InventoryProductXpo
        Dim filter As String = $"Id = {Id}"
        Return XpoServiceEx.Instance(Indigo.HisContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)(filter)
    End Function

#End Region

End Class