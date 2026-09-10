Imports System.Text.RegularExpressions
Imports DevExpress.XtraPrinting.BarCode

Public Class CtrInvoicesCapitatedEntities

#Region "Properties"

    ''' <summary>
    ''' Asigna el valor del consecutivo de la factura
    ''' </summary>
    ''' <returns></returns>
    Public Property SalesInvoiceConsecutive As String
        Get
            Return LSalesInvoiceConsecutive.Text
        End Get
        Set(value As String)
            LSalesInvoiceConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor total de los recaudos de la factura
    ''' </summary>
    ''' <returns></returns>
    Public Property CollectionsTotalValue As String
        Get
            Return LCollectionsTotalValue.Text
        End Get
        Set(value As String)
            LCollectionsTotalValue.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna el tipo de Liquidación que tiene el grupo de atención de la factura
    ''' </summary>
    Private _liquidationType As Integer
    Public Property LiquidationType As Integer
        Get
            Return _liquidationType
        End Get
        Set(value As Integer)
            _liquidationType = value
            Select Case value
                Case 1
                    LLiquidationType.Text = "Pago por servicios"
                Case 2
                    LLiquidationType.Text = "Capitación"
                Case 3
                    LLiquidationType.Text = "Factura global"
                Case 4
                    LLiquidationType.Text = "Capitación global"
                Case 5
                    LLiquidationType.Text = "Pago global prospectivo"
                Case Else
                    LLiquidationType.Text = "" 'Tipo de liquidación no valido
            End Select
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        SalesInvoiceConsecutive = Nothing
        LiquidationType = Nothing
    End Sub

#End Region

End Class
