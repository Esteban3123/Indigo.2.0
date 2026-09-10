Imports Presentation.Base
Imports Presentation.Controls

Public Interface IAgreementsRedemptionPoints
    Inherits ICrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece el id del cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la moneda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CurrencyId As Integer

    ''' <summary>
    ''' Obtiene o establece el cliente
    ''' </summary>
    ''' <value>
    ''' The thyrd party xpo.
    ''' </value>
    Property CustomerXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la moneda
    ''' </summary>
    ''' <value>
    ''' The thyrd party xpo.
    ''' </value>
    Property CurrencyXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property State As Integer


    ''' <summary>
    ''' Obtiene o establece la descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

End Interface
