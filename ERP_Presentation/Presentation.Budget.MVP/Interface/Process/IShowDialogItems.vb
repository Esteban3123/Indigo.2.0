#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
#End Region


Public Interface IShowDialogItems
    Inherits ICrudBase
#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

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
    ''' Obtiene o establece el id de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinancialSourceId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AlternativeCode As String

    ''' <summary>
    ''' Obtiene o establece el codigo CCPET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CCPETCodeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo CPC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CPCCodeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryName As String

    ''' <summary>
    ''' Obtiene o establece el padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ParentId As Integer?

    ''' <summary>
    ''' Obtiene o establece si el rubro pertenece al reporte de deficit o
    ''' equilibrio ptal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BalanceDeficit As Boolean?

    ''' <summary>
    ''' Obtiene o establece si maneja control de PAC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PAC As Boolean?

    ''' <summary>
    ''' Obtiene o establece si el rubro ingreso es de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IncomeCxP As Boolean?

    ''' <summary>
    ''' Obtiene o establece la Situación de Fondos
    ''' </summary>
    ''' <returns></returns>
    Property FundSituation As Char

    ''' <summary>
    ''' Obtiene o establece la vigencia
    ''' </summary>
    ''' <returns></returns>
    Property Validity As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la política pública
    ''' </summary>
    ''' <returns></returns>
    Property PublicPolicyId As Integer?

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Establece el datasource de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinancialSourceXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de los rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CCPETXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de los rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CPCCatalogXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de los rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ParentXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la política pública
    ''' </summary>
    ''' <returns></returns>
    Property PublicPolicyXpo As DevExpress.Xpo.XPInstantFeedbackSource
#End Region

End Interface
