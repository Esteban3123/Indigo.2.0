Imports Domain.Base.Entities
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid
Imports Domain.Entities

Public Class FormBase

    Public WithEvents BarraBotones As CtrBarraBotones

#Region "Fields"

    ''' <summary>
    ''' Constante error dependencia
    ''' </summary>
    Public Const ErrorDependencia As String = "-000"
    ''' <summary>
    ''' Constante error concurrencia
    ''' </summary>
    Public Const ErrorConcurrencia As String = "-999"
    ''' <summary>
    ''' Id de la entidad a abrir
    ''' </summary>
    Protected _idEntity As String
    ''' <summary>
    ''' Documento para indexar
    ''' </summary>
    Public _doc As IndexedDocument
    ''' <summary>
    ''' Función para construir el documento de indexación
    ''' </summary>
    Protected _funct As Func(Of IndexedDocument)
    ''' <summary>
    ''' Modelo para formulario base
    ''' </summary>
    Protected _model As MformBase
    ''' <summary>
    ''' Función para generar reporte
    ''' </summary>
    Protected _actionReport As Action(Of PrintReportAction, Integer, Integer, Object())
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Protected indigo As SessionValues
    ''' <summary>
    ''' Variable que contiene la instancia del formulario de busquedas
    ''' </summary>
    ''' <remarks></remarks>
    Public FormSearchObjects As FrmBusqueda
    ''' <summary>
    ''' Diccionario de secuencias numericas del frontal
    ''' </summary>
    Private _dicSequense As Dictionary(Of Int32, List(Of String))
    ''' <summary>
    ''' Variable para determinar si la busqueda es lanzada como principal del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public IsMainSearch As Boolean
    ''' <summary>
    ''' Diccionario para almacenar las columnas y saber cuales desbloquear luego
    ''' </summary>
    Dim _dictionaryColumns As Dictionary(Of Control, List(Of Columns.GridColumn))

    Public Event EventLoadOperatingUnit(listOperatingUnit As List(Of OperatingUnit))
#End Region


    ''' <summary>
    ''' Establecer mensaje en barra superior
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="type"></param>
    Sub ShowXtraMessage(ByVal message As String, ByVal type As ImagesXtraLabel, ByVal user As String)

        ''Dim user = message.Split("-")(0).Split(" ")(3).Trim()

        ''Me.XtraLabelText = message
        ''Me.XtraLabelImage = INDXtraImage.Images(type)
        'If Not user.Equals(String.Empty) AndAlso user.Equals(Indigo.UserIndigo) Then
        '    Exit Sub
        'End If
        'INDpanelMessage.Visible = True
        'Me.INDlbMessage.Text = message
        'Me.INDlbMessage.ImageAlignToText = ImageAlignToText.LeftCenter
        'Me.INDlbMessage.Appearance.Image = INDXtraImage.Images(type)

        'For Each item As DevExpress.XtraBars.BarItem In RibbonControl.Items

        '    If (item.Name <> "BargleStatus" AndAlso item.Name <> "BarBtnCerrar" AndAlso item.Name <> "BarLblXtraLabel" AndAlso item.Name <> "BarBtnDeshacer" AndAlso item.Name <> "BarBtnMinimizar") Then

        '        item.Enabled = False

        '    End If

        'Next

    End Sub

    ''' <summary>
    ''' Obtiene o asigna el id de la entidad a abrir
    ''' </summary>
    ''' <value>Id de la entidad</value>
    ''' <returns>El id de la entidad</returns>
    Public Property IdEntity As String
        Get
            Return Me._idEntity
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._idEntity = value
            Else
                Me._idEntity = String.Empty
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el diccionario de secuencias numericas almacenadas 
    ''' en memoria para el frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DicSequense As Dictionary(Of Int32, List(Of String))
        Get
            Return Me._dicSequense
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    Public Function GetDateServer() As DateTime
        Return DateTime.Now
    End Function

    Protected Function GetServerDate() As DateTime
        Return GetDateServer()
    End Function

    ''' <summary>
    ''' Ocurre cuando se ha recibido un Id de entidad y se hace necesario
    ''' consultarla por mandato del formulario base
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Event IdEntityLoaded(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Propiedad que obtiene o establece si el modo de vita por defecto es edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewModeEditHold As Boolean

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function ValidateControls() As Boolean
        'Dim res = Me.LayoutControls.ValidateFields()
        'If Not res.ResultStatus Then
        '    Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
        '    Return False
        'Else
        '    Return True
        'End If
    End Function

    ''' <summary>
    ''' Metodo para obtener un documento indexado
    ''' </summary>
    Public Async Sub GetDocumentIndexed(TextSearch As String)
        'Dim indexDocument As IndexedDocumentResultSet = Await Me._model.SearchIndexingDocument(TextSearch)
        'Me._doc = indexDocument.Results.FirstOrDefault
    End Sub

    ''' <summary>
    ''' Metodo para eliminar un documento indexado
    ''' </summary>
    Public Async Function DeleteDocumentIndexed() As Threading.Tasks.Task
        'Await Me._model.DeleteIndexingDocument(Me._doc)
    End Function

    ''' <summary>
    ''' Metodo para guardar el documento de indexación
    ''' </summary>
    Public Async Sub UpdateIndexedDocument(listDocuments As List(Of DocumentsStore))

    End Sub

    ''' <summary>
    ''' Metodo para visualizar el control de carga
    ''' </summary>
    ''' <param name="State">Estado</param>
    Public Overridable Sub AsyncLoader(ByVal State As Boolean)
        'INDmpbLoad.Visible = State
        'INDmpbLoad.SendToBack()
        'Me.ToolBars.Enabled = Not State
        'For i = 0 To Me.INDPanelControlBase.Controls.Count - 1
        '    If Me.INDPanelControlBase.Controls(i).GetType Is GetType(LayoutControl) Then
        '        Me.INDPanelControlBase.Controls(i).Enabled = Not State
        '        If State = False Then
        '            INDPanelControlBase.Controls(i).Focus()
        '        End If
        '        Exit For
        '    End If
        'Next

    End Sub

End Class