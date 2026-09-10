'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports System.Dynamic
Imports Infrastructure.Data.Xpo.CrystalRepository
#End Region
Public Class MAdmissionProduct
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' lista productos por admission (frmAdmissionProduct)
    ''' </summary>
    Public Function ListViewAdmissionProductServer(admissionNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListViewAdmissionProductServer(admissionNumber)
    End Function
    ''' <summary>
    ''' lista productos por admission (frmAdmissionProduct)
    ''' </summary>
    Public Function ListViewAdmissionProduct(admissionNumber As String) As XPCollection(Of InventoryViewAdmissionProductXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListViewAdmissionProduct(admissionNumber)
    End Function

    ''' <summary>
    ''' lista los detalles de productos por admission (frmAdmissionProduct)
    ''' </summary>
    Public Function ListViewAdmissionProductDetail(admissionNumber As String, functionalUnitId As Integer, productId As Integer) As XPCollection(Of InventoryViewAdmissionProductDetailXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListViewAdmissionProductDetail(admissionNumber, functionalUnitId, productId)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
