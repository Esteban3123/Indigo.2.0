'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Juan Carlos Bermudez
' Created          : 27-07-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IPortfolioReclassification
    Inherits IcrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene o establece el codigo del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' Obtiene o establece el tipo de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentType As Byte
    ''' <summary>
    ''' Obtiene o establece el id de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountReceivable As String
    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SourceAccount As String
    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TargetAccount As String
    ''' <summary>
    ''' Obtiene o establece el valor del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal
    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Byte

End Interface
