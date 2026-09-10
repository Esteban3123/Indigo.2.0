'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
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
Imports DevExpress.Data.Linq

#End Region

Public Interface IMedicalFeesCausation
    Inherits IcrudBase

    ''' <summary>
    ''' 
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
    ''' Establece el datasource del ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdmissionXpo As LinqInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceId As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha de causación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CausationDate As DateTime

    ''' <summary>
    ''' numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AdmissionNumber As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MemoNotes As String

End Interface
