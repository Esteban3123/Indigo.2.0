'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Rafael Eduardo Patiño
' Created          : 21/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface ITrazabilityPayments
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Propiedad que contiene el listado de proveedores 
    ''' </summary>
    Property SupplierXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para almacenar las cuentas por pagar de x proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayable As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o estbalece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplier As Integer?

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Property Type As Integer?

    ''' <summary>
    ''' Datasource de los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Property RefundsXpo As XPInstantFeedbackSource

#End Region

End Interface
