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

Public Class PProductFeeDetail

#Region "Variables"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IProductFeeDetail

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IProductFeeDetail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los productos que esten activados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitializeProducts()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProduct(True)
    End Function

#End Region

End Class