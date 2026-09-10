Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IBankConciliationConcepts
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

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
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el consecutivo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameM As String

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Property DocumentType As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante
    ''' </summary>
    ''' <returns></returns>
    Property VoucherType As Integer

End Interface
