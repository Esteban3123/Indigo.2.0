'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 22-05-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
#End Region

Public Interface ISettingBudget
    Inherits IcrudBase

    ''' <summary>
    ''' Habilitar interface con los otros modulos
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [enabled interface]; otherwise, <c>false</c>.
    ''' </value>
    Property EnabledInterface As Boolean?
    ''' <summary>
    ''' Crear reservas
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [create reserves]; otherwise, <c>false</c>.
    ''' </value>
    Property CreateReserves As Boolean?
    ''' <summary>
    ''' Crear cuentas por pagar
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [create payable accounts]; otherwise, <c>false</c>.
    ''' </value>
    Property CreatePayableAccounts As Boolean?
    ''' <summary>
    ''' Crear cuentas por cobrar
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [create receivable accounts]; otherwise, <c>false</c>.
    ''' </value>
    Property CreateReceivableAccounts As Boolean?
    ''' <summary>
    ''' Agrupar documentos por tercero
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [documents groupping]; otherwise, <c>false</c>.
    ''' </value>
    Property DocumentsGroupping As Boolean?

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' id de la Unidad Operativa
    ''' </summary>
    ''' <value>
    ''' The operating unit.
    ''' </value>
    Property idOperatingUnit As Integer
End Interface
