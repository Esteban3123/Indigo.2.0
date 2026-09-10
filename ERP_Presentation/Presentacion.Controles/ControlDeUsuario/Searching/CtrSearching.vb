
#Region "Imports"

Imports System.Diagnostics
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.XtraGrid.Views.Layout.ViewInfo
Imports DevExpress.XtraGrid.Views.Layout.Events
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass

#End Region

Public Class CtrSearching

#Region "Fields"

    ''' <summary>
    ''' Instancia al modelo
    ''' </summary>
    Private _model As MCtrSearching
    ''' <summary>
    ''' Lista de resultados en la busqueda de menus
    ''' </summary>
    Private _menuResults As List(Of Domain.Security.Entities.PermissionsFormsActive)
    ''' <summary>
    ''' Lista de resultados en la busqueda de registros
    ''' </summary>
    Private _regsResults As List(Of Domain.Base.Entities.IndexedDocument2)
    ''' <summary>
    ''' Lista de resultados en la busqueda de documentos
    ''' </summary>
    Private _docsResults As List(Of Domain.Base.Entities.IndexedDocument2)
    ''' <summary>
    ''' Lista de resultados en la busqueda al interior de documentos
    ''' </summary>
    Private _intoDocsResults As List(Of Domain.Base.Entities.IndexedDocument2)
    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _instanceSession As Infrastructure.CrossCutting.Base.SessionValues
    ''' <summary>
    ''' Bandera para saber si el control ya se cargo
    ''' </summary>
    Private _isLoaded As Boolean
    ''' <summary>
    ''' Bandera para controlar el cerrado del menu de filtros cuando el mouse salga
    ''' </summary>
    Private _flagMouseEnterInMenuFilter As Boolean
    ''' <summary>
    ''' Almacena el texto de la útima busqueda
    ''' </summary>
    Private _textLastSearch As String
    ''' <summary>
    ''' Encapsula el nombre del conjunto de datos que se encuentra en vista amplia
    ''' </summary>
    Private _viewLarge As String
    ''' <summary>
    ''' Colección de archivos temporales abiertos
    ''' </summary>
    Private _tempFileCollection As System.CodeDom.Compiler.TempFileCollection
    ''' <summary>
    ''' Conjunto maximo de resultados por pagina
    ''' </summary>
    Private _top As Int64
    ''' <summary>
    ''' Número de pagina actual
    ''' </summary>
    Private _page As Int64
    ''' <summary>
    ''' Maximo numero de resultados en la vista mequeña
    ''' </summary>
    Private _maxSmallResults As Int64
    ''' <summary>
    ''' Almacena el total de resultados para la ultima busqueda
    ''' </summary>
    Private _lastRawCount As Int64

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene un valor que indica si la útima busqueda retorno resultados
    ''' </summary>
    ''' <returns>Valor que indica si existen resultados</returns>
    Private ReadOnly Property HasResults As Boolean
        Get
            If Me._menuResults IsNot Nothing AndAlso Me._menuResults.Count > 0 Then
                Return True
            End If
            If Me._regsResults IsNot Nothing AndAlso Me._regsResults.Count > 0 Then
                Return True
            End If
            If Me._docsResults IsNot Nothing AndAlso Me._docsResults.Count > 0 Then
                Return True
            End If
            Return False
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Convierte una cadena de texto en codificación PascalCase
    ''' </summary>
    ''' <param name="str">Cadena</param>
    ''' <returns>Cadena convertida</returns>
    Private Function ToPascalCase(ByVal str As String) As String
        If (str Is Nothing) Then Return str
        If (str.Length < 2) Then Return str.ToUpper()

        Dim words() As String = str.Split(New Char() {}, StringSplitOptions.RemoveEmptyEntries)

        Dim result As String = ""
        For Each word As String In words
            If result.Trim().Equals(String.Empty) Then
                result &= word.Substring(0, 1).ToUpper() & word.Substring(1)
            Else
                result &= " " & word.Substring(0, 1).ToUpper() & word.Substring(1)
            End If
        Next word

        Return result
    End Function

    ''' <summary>
    ''' Oculta el menu de filtros
    ''' </summary>
    Private Sub HideMenuFilter()
        If Me._flagMouseEnterInMenuFilter Then
            Me.INDpccFilterMenu.Hide()
            Me.INDpccFilterMenu.Tag = "0"
            Me._flagMouseEnterInMenuFilter = False
        End If
    End Sub

    ''' <summary>
    ''' Asigna el foco a la caja de texto de busqueda
    ''' </summary>
    Public Sub SetFocusOnTextSearch()
        Me.SetAppearanceSearchPanelOnGotFocus()
    End Sub

    ''' <summary>
    ''' Cambia la apariencia del panel de busqueda cuando pierde el foco
    ''' </summary>
    Private Sub SetAppearanceSearchPanelOnLostFocus()
        If Me.HasResults Then
            Me.INDlblSearch.Text = Me.INDlblSearch.Tag.ToString().Split("|")(1)
            Me.INDlblSearch.ForeColor = Color.White
            Me.INDlblSearch.BackColor = Color.FromArgb(7, 69, 103)
            Me.INDpnlSubSearch.BackColor = Color.FromArgb(7, 69, 103)
            Me.INDtxtSearch.BackColor = Color.FromArgb(7, 69, 103)
            Me.INDtxtSearch.ForeColor = Color.White
            Me.INDtxtSearch.SelectionStart = 0
            Me.INDbtnSearch.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Cambia la apariencia del panel de busqueda cuando gana el foco
    ''' </summary>
    Private Sub SetAppearanceSearchPanelOnGotFocus()
        Me.INDlblSearch.Text = Me.INDlblSearch.Tag.ToString().Split("|")(0)
        Me.INDlblSearch.ForeColor = Color.FromArgb(7, 69, 103)
        Me.INDlblSearch.BackColor = Color.LightGray
        Me.INDpnlSubSearch.BackColor = Color.LightGray
        Me.INDtxtSearch.BackColor = Color.White
        Me.INDtxtSearch.ForeColor = Color.Black
        Me.INDbtnSearch.Visible = True
    End Sub

    ''' <summary>
    ''' Limpia la busqueda
    ''' </summary>
    Public Sub Clean()
        Me.INDgdvMenu.OptionsView.ShowViewCaption = True
        Me.INDgdvRegs.OptionsView.ShowViewCaption = True
        Me.INDgdvDocs.OptionsView.ShowViewCaption = True

        Me.INDlblMenuTotal.Text = String.Empty
        Me.INDlblRegsTotal.Text = String.Empty
        Me.INDlblDocsTotal.Text = String.Empty

        Me.INDlblMenuTotal.Visible = False
        Me.INDlblRegsTotal.Visible = False
        Me.INDlblDocsTotal.Visible = False

        Me._docsResults.Clear()
        Me._intoDocsResults.Clear()
        Me.INDgdcDocs.DataSource = Nothing

        Me._menuResults.Clear()
        Me.INDgdcMenu.DataSource = Nothing

        Me._regsResults.Clear()
        Me.INDgdcRegs.DataSource = Nothing

        Me.INDlblSearchTime.Text = String.Empty

        Me._textLastSearch = String.Empty

        Me._page = 0

        Me._lastRawCount = 0

        Me.INDtxtSearch.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Calcula la fecha inicial y final dependiendo del filtro seleccionado
    ''' </summary>
    ''' <returns>Tupla de fechas inicial y final</returns>
    Private Async Function CalcDateInterval() As Threading.Tasks.Task(Of Tuple(Of DateTime?, DateTime?))
        Dim serverDate As TimeSpan

        Try
            serverDate = Await Me._model.GetDateServer()
        Catch ex As Exception
            MessageIndigo.Show("El servicio de Indexación no esta en ejecución", MessageType.Warning, obtenerRecurso(Eresources.ComunesIndigoCrystal))
        End Try

        If Me.INDbtnLastHour.Tag IsNot Nothing AndAlso Me.INDbtnLastHour.Tag.ToString().Equals("1") Then
            Dim d As TimeSpan = serverDate - New TimeSpan(0, 60, 0)
            Return New Tuple(Of DateTime?, DateTime?)(New DateTime(d.Ticks), New DateTime(serverDate.Ticks))
        End If
        If Me.INDbtnToday IsNot Nothing AndAlso Me.INDbtnToday.Tag.ToString().Equals("1") Then
            Dim d As DateTime = New DateTime(serverDate.Ticks)
            d = New DateTime(d.Year, d.Month, d.Day, 0, 0, 0)
            Return New Tuple(Of DateTime?, DateTime?)(d, New DateTime(serverDate.Ticks))
        End If
        If Me.INDbtnWeek.Tag IsNot Nothing AndAlso Me.INDbtnWeek.Tag.ToString().Equals("1") Then
            Dim d As DateTime = (New DateTime(serverDate.Ticks)).AddDays(((Convert.ToInt32((New DateTime(serverDate.Ticks)).DayOfWeek)) * (-1)) + 1)
            Return New Tuple(Of DateTime?, DateTime?)(d, New DateTime(serverDate.Ticks))
        End If
        If Me.INDbtnMonth.Tag IsNot Nothing AndAlso Me.INDbtnMonth.Tag.ToString().Equals("1") Then
            Dim d As New DateTime(serverDate.Ticks)
            Return New Tuple(Of DateTime?, DateTime?)(New DateTime(d.Year, d.Month, 1, 0, 0, 0), d)
        End If
        If Me.INDbtnYear.Tag IsNot Nothing AndAlso Me.INDbtnYear.Tag.ToString().Equals("1") Then
            Dim d As New DateTime(serverDate.Ticks)
            Return New Tuple(Of DateTime?, DateTime?)(New DateTime(d.Year, 1, 1, 0, 0, 0), d)
        End If

        Return New Tuple(Of DateTime?, DateTime?)(Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Ejecuta la busqueda
    ''' </summary>
    Private Async Sub RunSearch(Optional ByVal ignore As Boolean = False, Optional ByVal isPaging As Boolean = False)
        If Not Me.INDtxtSearch.Text.Trim().Equals(String.Empty) Then
            If Not ignore Then
                If Me.INDtxtSearch.Text.Trim().ToLower().Equals(Me._textLastSearch.Trim().ToLower()) Then
                    Return
                End If
            End If

            If Not isPaging Then
                Me.INDgdvRegs.OptionsView.ShowViewCaption = True
                Me.INDlblRegsTotal.Text = String.Empty
                Me.INDlblRegsTotal.Visible = False
                Me._regsResults.Clear()
                Me.INDgdcRegs.DataSource = Nothing

                Me.INDlblDocsTotal.Visible = False
                Me.INDlblDocsTotal.Text = String.Empty
                Me.INDgdvDocs.OptionsView.ShowViewCaption = True
                Me._docsResults.Clear()
                Me._intoDocsResults.Clear()
                Me.INDgdcDocs.DataSource = Nothing

                Me._textLastSearch = String.Empty
                Me._page = 0
                Me._lastRawCount = 0
                Me.INDlblSearchTime.Text = String.Empty
            End If

            Dim totalResults As Int64 = 0
            Dim totalTime As New TimeSpan()
            Dim text As String = Me.INDtxtSearch.Text.Trim()

            Dim res = Me.SearchMenu(text.ToLower())
            totalResults += res.Item1
            totalTime = totalTime + res.Item2

            If Not ApplicationSetting.Instance.LoginAzure Then
                If Me.INDbtnContentDocuments.Tag IsNot Nothing AndAlso Me.INDbtnContentDocuments.Tag.ToString().Trim().Equals("1") Then
                    res = Await Me.SearchTextInDocs(text.Trim())
                    totalResults += res.Item1
                    totalTime = totalTime + res.Item2
                End If

                res = Await Me.SearchRegsAndDocs(text.ToLower())

                If Me._page = 0 Then
                    Me._lastRawCount = res.Item1
                End If

                totalTime = totalTime + res.Item2
                totalResults += res.Item1
            End If


            Me._textLastSearch = text

            If totalResults > 0 Then
                Me.INDlblSearchTime.Text = String.Format(Me.INDlblSearchTime.Tag.ToString().Trim(), totalResults.ToString("g", Me._instanceSession.Culture), totalTime.ToString("s\,ffff"))
            Else
                Me.INDlblSearchTime.Text = String.Empty
            End If
        Else
            Me.Clean()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la busqueda de menus
    ''' </summary>
    ''' <param name="text">Texto usado como patron de busqueda</param>
    Private Function SearchMenu(ByVal text As String) As Tuple(Of Int64, TimeSpan)
        Me.INDgdvMenu.OptionsView.ShowViewCaption = True
        Me.INDlblMenuTotal.Text = String.Empty
        Me.INDlblMenuTotal.Visible = False
        Me._menuResults.Clear()
        Me.INDgdcMenu.DataSource = Nothing


        Dim sp As New Stopwatch()
        sp.Reset()
        sp.Start()
        'Dim result = Me._instanceSession.ActiveForms.Where(Function(kv) kv.Value.Item1 IsNot Nothing AndAlso kv.Value.Item1.Trim().ToLower().Contains(text) AndAlso kv.Value.Item4.Contains("41")).ToList()
        Dim listContainsForm = Nothing
        'If ApplicationSetting.Instance.LoginAzure Then
        'Mejora para omitir caracteres especiales, buscar sin tildes.
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        Dim CultureCompareInfo = ConfigurationFile.Instance.Culture.InvariantCulture.CompareInfo
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        listContainsForm = Me._instanceSession.ListFormPermission.Where(Function(x) x.Module IsNot Nothing AndAlso (Not String.IsNullOrEmpty(x.AssemblyName) AndAlso x.AssemblyName <> "") AndAlso Not BaseClass.ListFormHide.Contains(x.Id) AndAlso CultureCompareInfo.IndexOf(x.Name.Trim.ToLower, text.ToLower, Globalization.CompareOptions.IgnoreNonSpace) > -1).ToList()
        Dim _titleName As String
        For Each kv In listContainsForm
            'Se ajuta para leer el title desde modelo
            _titleName = String.Format(" - Tipo: {0}", kv.TypeName)
            Me._menuResults.Add(New Domain.Security.Entities.PermissionsFormsActive With {.FormTag = kv.Id, .NameForm = kv.Name, .NameModule = kv.Module.Name, .NameGroup = String.Format("{0}{1}", kv.GroupForms, _titleName), .EnableEmbedded = False})
        Next
        'Else
        '    listContainsForm = Me._instanceSession.ListFormPermission.Where(Function(x) Not BaseClass.ListFormHide.Contains(x.Id) And x.Name.Trim.ToLower.Contains(text.ToLower)).ToList()
        '    For Each kv In listContainsForm
        '        Me._menuResults.Add(New Domain.Security.Entities.PermissionsFormsActive With {.FormTag = kv.Id, .NameForm = kv.Name, .NameModule = kv.Module.Name, .NameGroup = kv.GroupForms, .EnableEmbedded = False})
        '    Next
        'End If

        sp.Stop()
        If Me._menuResults.Count > 0 Then
            Me.INDgdvMenu.OptionsView.ShowViewCaption = False

            If Me._viewLarge.Trim().ToLower().Equals("menu") Then
                Me.INDgdcMenu.DataSource = Me._menuResults
                Me.INDlblMenuTotal.Text = String.Format(Me.INDlblMenuTotal.Tag.ToString().Trim(), Me._menuResults.Count)
            Else
                Me.INDgdcMenu.DataSource = Me._menuResults.Take(Me._maxSmallResults).ToList()
                Me.INDlblMenuTotal.Text = String.Format(Me.INDlblMenuTotal.Tag.ToString().Trim(), Me._menuResults.Count)
                Me.INDlblMenuTotal.Visible = True
            End If
            'Me.INDgdvMenu.ApplyFindFilter(text)
        End If
        Return New Tuple(Of Int64, TimeSpan)(Me._menuResults.Count.ToString("g", Me._instanceSession.Culture), sp.Elapsed)
    End Function

    ''' <summary>
    ''' Ejecuta la busqueda de texto dentro de cada documento
    ''' </summary>
    ''' <param name="text">Texto usado como patron de busqueda</param>
    Private Async Function SearchTextInDocs(ByVal text As String) As Threading.Tasks.Task(Of Tuple(Of Int64, TimeSpan))
        Dim sp As New Stopwatch()
        sp.Reset()
        sp.Start()

        Dim resultsIntoDocs = Await Me._model.getDocumentaFullText(text.Trim(), True, (Me._top * Me._page), Me.Top)

        sp.Stop()

        If resultsIntoDocs IsNot Nothing AndAlso resultsIntoDocs.Count > 0 Then
            Me.INDgdvDocs.OptionsView.ShowViewCaption = False
            If Me._page = 0 Then

                For Each doc As Domain.DocumentalSystem.Entities.DocumentsStore In resultsIntoDocs
                    Me._intoDocsResults.Add(New Domain.Base.Entities.IndexedDocument2 With {.DocumentType = 3, .IdForm = doc.IdForm, .IdEntity = doc.IdForm & "_" & doc.Id.ToString(), .Extension = doc.Type, .Title = Me.ToPascalCase("# " & doc.IdEntity & " " & doc.Name.Trim()), .Content = Me.ToPascalCase(doc.MetaData), .CreationUser = "Indigo Vituel", .Update = doc.AttachDate})
                    Dim res = Me._instanceSession.ListFormPermission.Where(Function(kv) kv.Id.Equals(doc.IdForm.ToString())).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        Me._intoDocsResults(Me._intoDocsResults.Count - 1).NameForm = Me.ToPascalCase(res(0).Name)
                    Else
                        Me._intoDocsResults(Me._intoDocsResults.Count - 1).NameForm = String.Empty
                    End If
                Next
                If Me._viewLarge.Trim().ToLower().Equals("docs") Then
                    Me.INDgdcDocs.DataSource = Me._intoDocsResults
                Else
                    Me.INDgdcDocs.DataSource = Me._intoDocsResults.Take(Me._maxSmallResults).ToList()
                    Me.INDlblDocsTotal.Visible = True
                End If
                Me.INDlblDocsTotal.Text = String.Format(Me.INDlblDocsTotal.Tag.ToString().Trim(), Me._intoDocsResults.Count)
            Else
                For Each doc As Domain.DocumentalSystem.Entities.DocumentsStore In resultsIntoDocs
                    Me._intoDocsResults.Add(New Domain.Base.Entities.IndexedDocument2 With {.DocumentType = 3, .IdForm = doc.IdForm, .IdEntity = doc.IdForm & "_" & doc.Id.ToString(), .Extension = doc.Type, .Title = Me.ToPascalCase("# " & doc.IdEntity & " " & doc.Name.Trim()), .Content = Me.ToPascalCase(doc.MetaData), .CreationUser = "Indigo Vituel", .Update = doc.AttachDate})
                    Dim res = Me._instanceSession.ListFormPermission.Where(Function(kv) kv.Id.Equals(doc.IdForm.ToString())).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        Me._intoDocsResults(Me._intoDocsResults.Count - 1).NameForm = Me.ToPascalCase(res(0).Name)
                    Else
                        Me._intoDocsResults(Me._intoDocsResults.Count - 1).NameForm = String.Empty
                    End If
                    If Me._viewLarge.Trim().ToLower().Equals("docs") Then
                        Me.INDgdcDocs.RefreshDataSource()
                    End If
                Next
                If Not Me._viewLarge.Trim().ToLower().Equals("docs") Then
                    Me.INDgdcDocs.DataSource = Me._intoDocsResults.Take(Me._maxSmallResults).ToList()
                    Me.INDlblDocsTotal.Visible = True
                End If
                Me.INDlblDocsTotal.Text = String.Format(Me.INDlblDocsTotal.Tag.ToString().Trim(), Me._intoDocsResults.Count)
            End If
            Return New Tuple(Of Int64, TimeSpan)(Me._intoDocsResults.Count.ToString("g", Me._instanceSession.Culture), sp.Elapsed)
        Else
            Return New Tuple(Of Int64, TimeSpan)(0.ToString("g", Me._instanceSession.Culture), sp.Elapsed)
        End If

    End Function

    ''' <summary>
    ''' Ejecuta la busqueda de registros
    ''' </summary>
    ''' <param name="text">Texto usado como patron de busqueda</param>
    Private Async Function SearchRegsAndDocs(ByVal text As String) As Threading.Tasks.Task(Of Tuple(Of Int64, TimeSpan))
        Dim sp As New Stopwatch()
        sp.Reset()
        sp.Start()

        Dim dss = Await Me.CalcDateInterval()
        Dim results As Domain.Base.Entities.IndexedDocumentResultSet2 = Nothing
        If Not String.IsNullOrEmpty(_instanceSession.VituelContainer) Then
            results = Await Me._model.SearchDocument(Me._instanceSession.VituelContainer, text.Trim(), dss.Item1, dss.Item2, (Me._top * Me._page), Me._top, True)
        End If

        sp.Stop()

        If results IsNot Nothing Then
            If results.RawCount > 0 Then

                'Creamos una lista auxiliar para poder eliminar los resultados a frontales sin permiso
                Dim aux As New List(Of Domain.Base.Entities.IndexedDocument2)()

                For Each d As Domain.Base.Entities.IndexedDocument2 In results.Results
                    Dim res = Me._instanceSession.ListFormPermission.Where(Function(kv) d.IdForm IsNot Nothing AndAlso Not d.IdForm.Trim().Equals(String.Empty) AndAlso kv.Id = CInt(d.IdForm)).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        d.NameForm = Me.ToPascalCase(res(0).Name)
                        d.Title = Me.ToPascalCase(d.Title.Trim())
                        d.Content = Me.ToPascalCase(d.Content.Trim())
                        'OJO!
                        '==============================================
                        'Tener en cuenta que el punto de la extension se esta
                        'quitando en el metodo ToDataTable de la entidad
                        'IndexedDocument porque pone problema al serializar
                        'el DataSet a Xml
                        If d.Extension Is Nothing Then
                            d.Extension = String.Empty
                        Else
                            d.Extension = If(d.Extension.Trim().StartsWith("."), d.Extension.ToLower(), "." & d.Extension.ToLower())
                        End If
                        '==============================================
                        aux.Add(d)
                    End If
                Next
                'Reasignamos de nuevo la lista filtrada
                results.Results = aux

                ''Aqui se convierte las cadenas en PascalCase
                'results.Results.ForEach(Sub(d)
                '                            Dim res = Me._instanceSession.ActiveForms.Where(Function(kv) kv.Key IsNot Nothing AndAlso kv.Key.Trim().Equals(d.IdForm.Trim())).ToList()
                '                            If res IsNot Nothing AndAlso res.Count > 0 Then
                '                                d.NameForm = Me.ToPascalCase(res(0).Value.Item1)
                '                            Else
                '                                d.NameForm = String.Empty
                '                            End If
                '                            d.Title = Me.ToPascalCase(d.Title.Trim())
                '                            d.Content = Me.ToPascalCase(d.Content.Trim())
                '                            'OJO!
                '                            '==============================================
                '                            'Tener en cuenta que el punto de la extension se esta
                '                            'quitando en el metodo ToDataTable de la entidad
                '                            'IndexedDocument porque pone problema al serializar
                '                            'el DataSet a Xml
                '                            d.Extension = "." & d.Extension.ToLower()
                '                            '==============================================
                '                        End Sub)

                'Aqui obtenemos los registros
                If Me._page = 0 Then
                    Me._regsResults.AddRange(results.Results.Where(Function(d) d.DocumentType <> Domain.Base.Entities.IndexedDocumentType.ScannedDocument).ToList())

                    If Me._regsResults.Count > 0 Then
                        Me.INDgdvRegs.OptionsView.ShowViewCaption = False
                        If Me._viewLarge.Trim().ToLower().Equals("regs") Then
                            Me.INDgdcRegs.DataSource = Me._regsResults
                            Me.INDlblRegsTotal.Text = String.Format(Me.INDlblRegsTotal.Tag.ToString().Trim(), Me._regsResults.Count)
                        Else
                            Me.INDgdcRegs.DataSource = Me._regsResults.Take(Me._maxSmallResults).ToList()
                            Me.INDlblRegsTotal.Text = String.Format(Me.INDlblRegsTotal.Tag.ToString().Trim(), Me._regsResults.Count)
                            Me.INDlblRegsTotal.Visible = True
                        End If
                    End If

                    'Aqui obtenemos los documentos
                    If Me.INDbtnContentDocuments.Tag Is Nothing OrElse Me.INDbtnContentDocuments.Tag.ToString().Trim().Equals("0") Then
                        Me._docsResults.AddRange(results.Results.Where(Function(d) d.DocumentType = Domain.Base.Entities.IndexedDocumentType.ScannedDocument).ToList())

                        If Me._docsResults.Count > 0 Then
                            Me.INDgdvDocs.OptionsView.ShowViewCaption = False
                            If Me._viewLarge.Trim().ToLower().Equals("docs") Then
                                Me.INDgdcDocs.DataSource = Me._docsResults
                                Me.INDlblDocsTotal.Text = String.Format(Me.INDlblDocsTotal.Tag.ToString().Trim(), Me._docsResults.Count)
                            Else
                                Me.INDgdcDocs.DataSource = Me._docsResults.Take(Me._maxSmallResults).ToList()
                                Me.INDlblDocsTotal.Text = String.Format(Me.INDlblDocsTotal.Tag.ToString().Trim(), Me._docsResults.Count)
                                Me.INDlblDocsTotal.Visible = True
                            End If
                        End If
                    End If
                Else
                    Me._regsResults.AddRange(results.Results.Where(Function(d) d.DocumentType <> Domain.Base.Entities.IndexedDocumentType.ScannedDocument).ToList())

                    If Me._regsResults.Count > 0 Then
                        Me.INDgdvRegs.OptionsView.ShowViewCaption = False
                        If Me._viewLarge.Trim().ToLower().Equals("regs") Then
                            Me.INDgdcRegs.RefreshDataSource()
                        Else
                            Me.INDgdcRegs.DataSource = Me._regsResults.Take(Me._maxSmallResults).ToList()
                            Me.INDlblRegsTotal.Visible = True
                        End If
                        Me.INDlblRegsTotal.Text = String.Format(Me.INDlblRegsTotal.Tag.ToString().Trim(), Me._regsResults.Count)
                    End If

                    'Aqui obtenemos los documentos
                    If Me.INDbtnContentDocuments.Tag Is Nothing OrElse Me.INDbtnContentDocuments.Tag.ToString().Trim().Equals("0") Then
                        Me._docsResults.AddRange(results.Results.Where(Function(d) d.DocumentType = Domain.Base.Entities.IndexedDocumentType.ScannedDocument).ToList())

                        If Me._docsResults.Count > 0 Then
                            Me.INDgdvDocs.OptionsView.ShowViewCaption = False
                            If Me._viewLarge.Trim().ToLower().Equals("docs") Then
                                Me.INDgdcDocs.RefreshDataSource()
                            Else
                                Me.INDgdcDocs.DataSource = Me._docsResults.Take(Me._maxSmallResults).ToList()
                                Me.INDlblDocsTotal.Visible = True
                            End If
                            Me.INDlblDocsTotal.Text = String.Format(Me.INDlblDocsTotal.Tag.ToString().Trim(), Me._docsResults.Count)
                        End If
                    End If
                End If

                'Me.INDgdvRegs.ApplyFindFilter(text)
                'Me.INDgdvDocs.ApplyFindFilter(text)
                Return New Tuple(Of Int64, TimeSpan)(results.RawCount.ToString("g", Me._instanceSession.Culture), results.ElapsedTime)
            Else
                Return New Tuple(Of Int64, TimeSpan)(0.ToString("g", Me._instanceSession.Culture), results.ElapsedTime)
            End If
        Else
            Return New Tuple(Of Int64, TimeSpan)(0.ToString("g", Me._instanceSession.Culture), sp.Elapsed)
        End If
    End Function

    ''' <summary>
    ''' Realiza la asignación inicial de imagenes a las opciones del menu de filtros
    ''' </summary>
    Private Sub AssignImagesToMenuFilter()
        Me.INDbtnContentDocuments.Image = Me.INDimcMenuFilter.Images(0)
        Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
        Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
        Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
        Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
        Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)
    End Sub

    ''' <summary>
    ''' Asigna el selector de foco al conjunto de resultados correspondiente al nombre de la vista
    ''' </summary>
    ''' <param name="nameGridView">Nombre de la vista a quien se le asigna el selector</param>
    Private Sub AssignSelectorToGroupResults(Optional ByVal nameGridView As String = "")
        If Me._viewLarge.Trim().Equals(String.Empty) Then
            Select Case nameGridView.Trim().ToLower()
                Case "indgdvmenu"
                    Me.INDlnMenuSelector.Visible = True
                    Me.INDlnRegsSelector.Visible = False
                    Me.INDlnDocsSelector.Visible = False
                Case "indgdvregs"
                    Me.INDlnMenuSelector.Visible = False
                    Me.INDlnRegsSelector.Visible = True
                    Me.INDlnDocsSelector.Visible = False
                Case "indgdvdocs"
                    Me.INDlnMenuSelector.Visible = False
                    Me.INDlnRegsSelector.Visible = False
                    Me.INDlnDocsSelector.Visible = True
                Case Else
                    Me.INDlnMenuSelector.Visible = False
                    Me.INDlnRegsSelector.Visible = False
                    Me.INDlnDocsSelector.Visible = False
            End Select
        Else
            Me.INDlnMenuSelector.Visible = False
            Me.INDlnRegsSelector.Visible = False
            Me.INDlnDocsSelector.Visible = False
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "General"

    ''' <summary>
    ''' Aqui se cambia el color del sombreado en las palabras encontradas en las rejillas
    ''' </summary>
    Private Sub INDgdvMenu_CustomDrawCardFieldValue(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgdvMenu.CustomDrawCardFieldValue, INDgdvDocs.CustomDrawCardFieldValue, INDgdvRegs.CustomDrawCardFieldValue
        Dim view As LayoutView = TryCast(sender, LayoutView)
        If view IsNot Nothing AndAlso view.OptionsFind.HighlightFindResults AndAlso view.FindFilterText IsNot Nothing AndAlso Not view.FindFilterText.Trim().Equals(String.Empty) Then
            Dim index = e.DisplayText.ToLower().IndexOf(view.FindFilterText)
            If index > -1 Then
                e.Appearance.FillRectangle(e.Cache, e.Bounds)
                e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, view.FindFilterText, e.Appearance, Color.LightGray, Color.FromArgb(163, 66, 167, 255), True, index)
                e.Handled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se cambia el cursor cuando pasa por el campo del titulo
    ''' </summary>
    Private Sub INDgdvMenu_MouseMove(sender As Object, e As MouseEventArgs) Handles INDgdvMenu.MouseMove
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvMenu.CalcHitInfo(e.Location)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso hit.HitField.FieldName.Trim().Equals("NameForm") Then
            Me.INDgdcMenu.Cursor = Cursors.Hand
        Else
            Me.INDgdcMenu.Cursor = Cursors.Default
        End If
        Me.HideMenuFilter()
    End Sub
    Private Sub INDgdvRegs_MouseMove(sender As Object, e As MouseEventArgs) Handles INDgdvRegs.MouseMove
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvRegs.CalcHitInfo(e.Location)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso hit.HitField.FieldName.Trim().Equals("Title") Then
            Me.INDgdcRegs.Cursor = Cursors.Hand
        Else
            Me.INDgdcRegs.Cursor = Cursors.Default
        End If
        Me.HideMenuFilter()
    End Sub
    Private Sub INDgdvDocs_MouseMove(sender As Object, e As MouseEventArgs) Handles INDgdvDocs.MouseMove
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvDocs.CalcHitInfo(e.Location)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso hit.HitField.FieldName.Trim().Equals("Title") Then
            Me.INDgdcDocs.Cursor = Cursors.Hand
        Else
            Me.INDgdcDocs.Cursor = Cursors.Default
        End If
        Me.HideMenuFilter()
    End Sub

    ''' <summary>
    ''' Aqui se reliza la animación de los selectores sobre los titulos de cada conjunto de resultados
    ''' </summary>
    Private Sub Results_GotFocus(sender As Object, e As EventArgs) Handles INDgdvMenu.GotFocus, INDgdvRegs.GotFocus, INDgdvDocs.GotFocus
        If Me._isLoaded Then
            Me.SetAppearanceSearchPanelOnLostFocus()
            Me.AssignSelectorToGroupResults(CType(sender, DevExpress.XtraGrid.Views.Layout.LayoutView).Name)
            CType(sender, DevExpress.XtraGrid.Views.Layout.LayoutView).FocusedColumn = CType(sender, DevExpress.XtraGrid.Views.Layout.LayoutView).VisibleColumns(1)
            CType(sender, DevExpress.XtraGrid.Views.Layout.LayoutView).FocusedRowHandle = 0
        End If
    End Sub
    Private Sub Results_LostFocus(sender As Object, e As EventArgs) Handles INDgdvMenu.LostFocus, INDgdvRegs.LostFocus, INDgdvDocs.LostFocus
        If Me._isLoaded Then
            Me.AssignSelectorToGroupResults()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la carga e inicialización de controles
    ''' </summary>
    Private Sub CtrSearching_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me._model = New MCtrSearching()

        Me._textLastSearch = String.Empty
        Me._instanceSession = Infrastructure.CrossCutting.Base.SessionValues.Instance
        Me._menuResults = New List(Of Domain.Security.Entities.PermissionsFormsActive)()
        Me._regsResults = New List(Of Domain.Base.Entities.IndexedDocument2)()
        Me._docsResults = New List(Of Domain.Base.Entities.IndexedDocument2)()
        Me._intoDocsResults = New List(Of Domain.Base.Entities.IndexedDocument2)()
        Me._viewLarge = String.Empty
        Me._tempFileCollection = New CodeDom.Compiler.TempFileCollection()
        Me._flagMouseEnterInMenuFilter = False
        Me._top = 5
        Me._page = 0
        Me._lastRawCount = 0
        Me._maxSmallResults = 3

        Me.AssignImagesToMenuFilter()
        Me.INDtxtSearch.Focus()
        Me._isLoaded = True
    End Sub

    ''' <summary>
    ''' Aqui cambia la extensión de documentos no conocidos para poder cambiar el icono
    ''' en el repositorio de imagen de la rejilla
    ''' </summary>
    Private Sub INDgdvDocs_CustomRowCellEdit(sender As Object, e As DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCustomRowCellEditEventArgs) Handles INDgdvDocs.CustomRowCellEdit
        If e.Column.Equals(Me.ColDocsTile) Then
            Dim exts = (From ex As ImageComboBoxItem In Me.RepimlTileDocs.Items Select ex.Value).ToList()
            If Not exts.Contains(e.CellValue) Then
                Me.INDgdvDocs.SetRowCellValue(e.RowHandle, e.Column, ".otro")
            End If
        End If
    End Sub

#End Region

#Region "Searching"

    ''' <summary>
    ''' Aqui se ejecuta la busqueda
    ''' </summary>
    Private Sub INDtmrRelaySearch_Tick(sender As Object, e As EventArgs) Handles INDtmrRelaySearch.Tick
        Me.INDtmrRelaySearch.Stop()
        Me.RunSearch()
    End Sub

    Private Sub INDtxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtSearch.KeyDown
        Me.INDtmrRelaySearch.Stop()
    End Sub
    Private Sub INDtxtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles INDtxtSearch.KeyUp
        Me.INDtmrRelaySearch.Start()
    End Sub
    Private Sub INDbtnSearch_Click(sender As Object, e As EventArgs) Handles INDbtnSearch.Click
        Me.RunSearch(True)
        Me.INDtxtSearch.Focus()
    End Sub
    Private Sub INDtxtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles INDtxtSearch.KeyPress
        If e.KeyChar.Equals(Convert.ToChar(Keys.Enter)) Then
            Me.INDtmrRelaySearch.Stop()
            Me.RunSearch(True)
            Me.SetAppearanceSearchPanelOnLostFocus()
            Select Case Me._viewLarge.Trim().ToLower()
                Case "regs"
                    Me.INDgdvRegs.Focus()
                Case "docs"
                    Me.INDgdvDocs.Focus()
                Case Else
                    Me.INDgdvMenu.Focus()
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la invocación de nuevos resultados
    ''' </summary>
    Private Sub RegsAndDocs_VisibleRecordIndexChanged(sender As Object, e As LayoutViewVisibleRecordIndexChangedEventArgs) Handles INDgdvRegs.VisibleRecordIndexChanged, INDgdvDocs.VisibleRecordIndexChanged
        If Me.INDtxtSearch.Text.Trim().ToLower().Equals(Me._textLastSearch.Trim().ToLower()) AndAlso Me._lastRawCount > (Me._regsResults.Count + Me._docsResults.Count) Then
            If Me._viewLarge.Trim().ToLower().Equals("docs") OrElse Me._viewLarge.Trim().ToLower().Equals("regs") Then
                If e.VisibleRecordIndex = (Me._docsResults.Count - 1) OrElse e.VisibleRecordIndex = (Me._regsResults.Count - 1) Then
                    Me._page += 1
                    Me.RunSearch(True, True)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Results"

    ''' <summary>
    ''' Aqui se retorna a la vista normal del conjunto de resultados
    ''' </summary>
    Private Sub INDbtnBack_Click(sender As Object, e As EventArgs) Handles INDbtnBack.Click
        Select Case Me._viewLarge
            Case "menu"
                Me.INDgdcMenu.DataSource = Me._menuResults.Take(Me._maxSmallResults).ToList()
                Me.INDgdvMenu.OptionsMultiRecordMode.StretchCardToViewWidth = True
                Me.INDgdvMenu.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
                Me.INDlblMenuTotal.Visible = True
                Me.INDlblMenuTitle.Text = Me.INDlblTitle.Text
                Me.INDlnMenuSelector.Visible = True
                Me.INDlblTitle.Text = String.Empty
                Me.INDbtnBack.Visible = False
                Me.INDlyciRegsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyciDocsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me._viewLarge = String.Empty
            Case "regs"
                Me.INDgdcRegs.DataSource = Me._regsResults.Take(Me._maxSmallResults).ToList()
                Me.INDgdvRegs.OptionsMultiRecordMode.StretchCardToViewWidth = True
                Me.INDgdvRegs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
                Me.INDlblRegsTotal.Visible = True
                Me.INDlblRegsTitle.Text = Me.INDlblTitle.Text
                Me.INDlnRegsSelector.Visible = True
                Me.INDlblTitle.Text = String.Empty
                Me.INDbtnBack.Visible = False
                Me.INDlyciMenuResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyciDocsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me._viewLarge = String.Empty
            Case "docs"
                Me.INDgdcDocs.DataSource = Me._docsResults.Take(Me._maxSmallResults).ToList()
                Me.INDgdvDocs.OptionsMultiRecordMode.StretchCardToViewWidth = True
                Me.INDgdvDocs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
                Me.INDlblDocsTotal.Visible = True
                Me.INDlblDocsTitle.Text = Me.INDlblTitle.Text
                Me.INDlnDocsSelector.Visible = True
                Me.INDlblTitle.Text = String.Empty
                Me.INDbtnBack.Visible = False
                Me.INDlyciMenuResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyciRegsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me._viewLarge = String.Empty
        End Select
    End Sub

    ''' <summary>
    ''' Aqui se muestra el resultado total de menus
    ''' </summary>
    Private Sub INDlblMenuTotal_Click(sender As Object, e As EventArgs) Handles INDlblMenuTotal.Click
        Me._viewLarge = "menu"
        Me.INDlyciRegsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlyciDocsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlblMenuTotal.Visible = False
        Me.INDlblTitle.Text = Me.INDlblMenuTitle.Text
        Me.INDlblMenuTitle.Text = String.Empty
        Me.INDlnMenuSelector.Visible = False
        Me.INDbtnBack.Visible = True
        Me.INDgdcMenu.DataSource = Me._menuResults
        Me.INDgdvMenu.OptionsMultiRecordMode.StretchCardToViewWidth = False
        Me.INDgdvMenu.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiColumn
    End Sub

    ''' <summary>
    ''' Aqui se muestra el resultado total de gegistros
    ''' </summary>
    Private Sub INDlblRegsTotal_Click(sender As Object, e As EventArgs) Handles INDlblRegsTotal.Click
        Me._viewLarge = "regs"
        Me.INDlyciMenuResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlyciDocsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlblRegsTotal.Visible = False
        Me.INDlblTitle.Text = Me.INDlblRegsTitle.Text
        Me.INDlblRegsTitle.Text = String.Empty
        Me.INDlnRegsSelector.Visible = False
        Me.INDbtnBack.Visible = True
        Me.INDgdcRegs.DataSource = Me._regsResults
        Me.INDgdvRegs.OptionsMultiRecordMode.StretchCardToViewWidth = False
        Me.INDgdvRegs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiColumn
        If Me._lastRawCount > (Me._regsResults.Count + Me._docsResults.Count) Then
            Me._page += 1
            Me.RunSearch(True, True)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se muestra el resultado total de menus
    ''' </summary>
    Private Sub INDlblDocsTotal_Click(sender As Object, e As EventArgs) Handles INDlblDocsTotal.Click
        Me._viewLarge = "docs"
        Me.INDlyciRegsResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlyciMenuResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlblDocsTotal.Visible = False
        Me.INDlblTitle.Text = Me.INDlblDocsTitle.Text
        Me.INDlblDocsTitle.Text = String.Empty
        Me.INDlnDocsSelector.Visible = False
        Me.INDbtnBack.Visible = True
        Me.INDgdcDocs.DataSource = Me._docsResults
        Me.INDgdvDocs.OptionsMultiRecordMode.StretchCardToViewWidth = False
        Me.INDgdvDocs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiColumn
        If Me._lastRawCount > (Me._regsResults.Count + Me._docsResults.Count) Then
            Me._page += 1
            Me.RunSearch(True, True)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se mueve el foco al conjunto de resultados correspondiente a la posición
    ''' </summary>
    Private Sub INDgdvMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles INDgdvMenu.KeyDown
        If e.KeyCode.Equals(Keys.Right) AndAlso Me._viewLarge.Trim().Equals(String.Empty) Then
            Me.INDgdvRegs.Focus()
        ElseIf e.KeyCode.Equals(Keys.Enter) Then
            Me.OpenRegMenu()
        End If
    End Sub
    Private Sub INDgdvRegs_KeyDown(sender As Object, e As KeyEventArgs) Handles INDgdvRegs.KeyDown
        If e.KeyCode.Equals(Keys.Right) AndAlso Me._viewLarge.Trim().Equals(String.Empty) Then
            Me.INDgdvDocs.Focus()
        ElseIf e.KeyCode.Equals(Keys.Left) AndAlso Me._viewLarge.Trim().Equals(String.Empty) Then
            Me.INDgdvMenu.Focus()
        ElseIf e.KeyCode.Equals(Keys.Enter) Then
            Me.OpenRegRegs()
        End If
    End Sub
    Private Async Sub INDgdvDocs_KeyDown(sender As Object, e As KeyEventArgs) Handles INDgdvDocs.KeyDown
        If e.KeyCode.Equals(Keys.Left) AndAlso Me._viewLarge.Trim().Equals(String.Empty) Then
            Me.INDgdvRegs.Focus()
        ElseIf e.KeyCode.Equals(Keys.Enter) Then
            Me.OpenRegDocs()
        End If
    End Sub

#End Region

#Region "Search Panel"

    ''' <summary>
    ''' Aqui se cambia el icono del boton de busqueda
    ''' </summary>
    Private Sub INDbtnSearch_MouseUp(sender As Object, e As MouseEventArgs) Handles INDbtnSearch.MouseUp
        Me.INDbtnSearch.Image = Me.INDimcButtonSearch.Images(0)
    End Sub

    ''' <summary>
    ''' Aqui se cambia el icono del boton de busqueda
    ''' </summary>
    Private Sub INDbtnSearch_MouseDown(sender As Object, e As MouseEventArgs) Handles INDbtnSearch.MouseDown
        Me.INDbtnSearch.Image = Me.INDimcButtonSearch.Images(1)
    End Sub

    ''' <summary>
    ''' Aqui se selecciona todo el texto del control
    ''' </summary>
    Private Sub INDtxtSearch_GotFocus(sender As Object, e As EventArgs) Handles INDtxtSearch.GotFocus
        Me.SetAppearanceSearchPanelOnGotFocus()
    End Sub
    Private Sub INDtxtSearch_DoubleClick(sender As Object, e As EventArgs) Handles INDtxtSearch.DoubleClick
        Me.INDtxtSearch.SelectionStart = 0
        Me.INDtxtSearch.SelectAll()
    End Sub

    ''' <summary>
    ''' Aqui se le da efecto al label de filtros
    ''' </summary>
    Private Sub INDlblFilters_MouseEnter(sender As Object, e As EventArgs) Handles INDlblFilters.MouseEnter
        Me.INDlblFilters.ForeColor = Color.Black
    End Sub
    Private Sub INDlblFilters_MouseLeave(sender As Object, e As EventArgs) Handles INDlblFilters.MouseLeave
        Me.INDlblFilters.ForeColor = Color.Gray
    End Sub

#End Region

#Region "Menu Filter"

    ''' <summary>
    ''' Efectos del menu
    ''' </summary>
    Private Sub INDlblFilters_Click(sender As Object, e As EventArgs) Handles INDlblFilters.Click
        If Me.INDpccFilterMenu.Tag.ToString().Trim().Equals("0") Then
            Me.INDpccFilterMenu.Location = New Point(Me.INDlblFilters.Location.X - 14, 135)
            Me.INDpccFilterMenu.Show()
            Me.INDpccFilterMenu.Tag = "1"
        Else
            Me.INDpccFilterMenu.Hide()
            Me.INDpccFilterMenu.Tag = "0"
        End If
    End Sub
    Private Sub INDButtonsFilter_Click(sender As Object, e As EventArgs) Handles INDbtnNoFilter.Click, INDbtnLastHour.Click, INDbtnToday.Click, INDbtnWeek.Click, INDbtnMonth.Click, INDbtnYear.Click
        Select Case CType(sender, Button).Name
            Case "INDbtnNoFilter"
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnNoFilter.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(1)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)

                Me.INDbtnNoFilter.Tag = "1"
                Me.INDbtnLastHour.Tag = "0"
                Me.INDbtnToday.Tag = "0"
                Me.INDbtnWeek.Tag = "0"
                Me.INDbtnMonth.Tag = "0"
                Me.INDbtnYear.Tag = "0"
            Case "INDbtnLastHour"
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnLastHour.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(1)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)

                Me.INDbtnNoFilter.Tag = "0"
                Me.INDbtnLastHour.Tag = "1"
                Me.INDbtnToday.Tag = "0"
                Me.INDbtnWeek.Tag = "0"
                Me.INDbtnMonth.Tag = "0"
                Me.INDbtnYear.Tag = "0"
            Case "INDbtnToday"
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnToday.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(1)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)

                Me.INDbtnNoFilter.Tag = "0"
                Me.INDbtnLastHour.Tag = "0"
                Me.INDbtnToday.Tag = "1"
                Me.INDbtnWeek.Tag = "0"
                Me.INDbtnMonth.Tag = "0"
                Me.INDbtnYear.Tag = "0"
            Case "INDbtnWeek"
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnWeek.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(1)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)

                Me.INDbtnNoFilter.Tag = "0"
                Me.INDbtnLastHour.Tag = "0"
                Me.INDbtnToday.Tag = "0"
                Me.INDbtnWeek.Tag = "1"
                Me.INDbtnMonth.Tag = "0"
                Me.INDbtnYear.Tag = "0"
            Case "INDbtnMonth"
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnMonth.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(1)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(0)

                Me.INDbtnNoFilter.Tag = "0"
                Me.INDbtnLastHour.Tag = "0"
                Me.INDbtnToday.Tag = "0"
                Me.INDbtnWeek.Tag = "0"
                Me.INDbtnMonth.Tag = "1"
                Me.INDbtnYear.Tag = "0"
            Case Else
                Me.INDlblFilters.Text = String.Format(Me.INDlblFilters.Tag.ToString().Trim(), Me.INDbtnYear.Text.Trim())

                Me.INDbtnNoFilter.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnLastHour.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnToday.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnWeek.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnMonth.Image = Me.INDimcMenuFilter.Images(0)
                Me.INDbtnYear.Image = Me.INDimcMenuFilter.Images(1)

                Me.INDbtnNoFilter.Tag = "0"
                Me.INDbtnLastHour.Tag = "0"
                Me.INDbtnToday.Tag = "0"
                Me.INDbtnWeek.Tag = "0"
                Me.INDbtnMonth.Tag = "0"
                Me.INDbtnYear.Tag = "1"
        End Select
        Me.INDpccFilterMenu.Hide()
        Me.INDpccFilterMenu.Tag = "0"
        Me._flagMouseEnterInMenuFilter = False
        Me.RunSearch(True)
        Me.SetAppearanceSearchPanelOnLostFocus()
        Me.INDtxtSearch.Focus()
    End Sub
    Private Sub INDbtnContentDocuments_Click(sender As Object, e As EventArgs) Handles INDbtnContentDocuments.Click
        If Me.INDbtnContentDocuments.Tag.ToString().Trim().Equals("1") Then
            Me.INDbtnContentDocuments.Tag = "0"
            Me.INDbtnContentDocuments.Image = Me.INDimcMenuFilter.Images(0)
        Else
            Me.INDbtnContentDocuments.Tag = "1"
            Me.INDbtnContentDocuments.Image = Me.INDimcMenuFilter.Images(1)
        End If
        Me.INDpccFilterMenu.Hide()
        Me.INDpccFilterMenu.Tag = "0"
        Me._flagMouseEnterInMenuFilter = False
        Me.INDtxtSearch.Focus()
    End Sub

    ''' <summary>
    ''' Aqui se controla ocultar el menu cuando el mouse salga de él
    ''' </summary>
    Private Sub INDButtonsFilter_MouseEnter(sender As Object, e As EventArgs) Handles INDbtnNoFilter.MouseEnter, INDbtnLastHour.MouseEnter, INDbtnToday.MouseEnter, INDbtnWeek.MouseEnter, INDbtnMonth.MouseEnter, INDbtnYear.MouseEnter, INDbtnContentDocuments.MouseEnter
        Me._flagMouseEnterInMenuFilter = True
    End Sub
    Private Sub INDpccFilterMenu_MouseLeave(sender As Object, e As EventArgs) Handles INDpccFilterMenu.MouseLeave
        Me.HideMenuFilter()
    End Sub
    Private Sub INDHideMenuFilter_MouseMove(sender As Object, e As MouseEventArgs) Handles INDlblFilters.MouseMove, INDlblTitle.MouseMove, INDpnlMenuTitle.MouseMove, INDpnlRegsTitle.MouseMove, INDpnlDocsTitle.MouseMove, INDlblRegsTitle.MouseMove, INDlblMenuTitle.MouseMove, INDlblDocsTitle.MouseMove
        Me.HideMenuFilter()
    End Sub

#End Region

#Region "QueryOpenForm"

    ''' <summary>
    ''' Abre un resgistro en el conjunto de resultados de menu
    ''' </summary>
    Private Sub OpenRegMenu()
        Dim obj = CType(Me.INDgdvMenu.GetRow(Me.INDgdvMenu.FocusedRowHandle), Domain.Security.Entities.PermissionsFormsActive)
        If obj IsNot Nothing AndAlso obj.FormTag IsNot Nothing AndAlso Not obj.FormTag.Trim().Equals(String.Empty) Then
            RaiseEvent QueryOpenForm(Me, New QueryOpenFormEventArgs(obj.FormTag, String.Empty))
        End If
    End Sub

    ''' <summary>
    ''' Abre un resgistro en el conjunto de resultados de registros
    ''' </summary>
    Private Sub OpenRegRegs()
        Dim obj = CType(Me.INDgdvRegs.GetRow(Me.INDgdvRegs.FocusedRowHandle), Domain.Base.Entities.IndexedDocument2)
        If obj IsNot Nothing AndAlso obj.IdForm IsNot Nothing AndAlso Not obj.IdForm.Trim().Equals(String.Empty) Then
            RaiseEvent QueryOpenForm(Me, New QueryOpenFormEventArgs(obj.IdForm, obj.IdEntity.Split("_")(1)))
        End If
    End Sub

    ''' <summary>
    ''' Abre un resgistro en el conjunto de resultados de documentos
    ''' </summary>
    Private Async Sub OpenRegDocs()
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        Dim obj = CType(Me.INDgdvDocs.GetRow(Me.INDgdvDocs.FocusedRowHandle), Domain.Base.Entities.IndexedDocument2)
        If obj IsNot Nothing AndAlso obj.IdEntity IsNot Nothing AndAlso Not obj.IdEntity.Trim().Equals(String.Empty) AndAlso obj.Extension IsNot Nothing AndAlso Not obj.Extension.Trim().Equals(String.Empty) Then
            'Dim tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath, System.IO.Path.GetRandomFileName)
            Dim tempFilePath = System.IO.Path.Combine(Window.Utils.TemporalFolder(), System.IO.Path.GetRandomFileName)
            tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, System.IO.Path.GetExtension(obj.Extension))
            Me._tempFileCollection.AddFile(tempFilePath, keepFile:=False)
            Using sqlFileStream = Await Me._model.getData(obj.IdEntity.Split("_")(1)),
                                          localFileStream As New IO.FileStream(tempFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                sqlFileStream.CopyTo(localFileStream)
            End Using
            Process.Start(tempFilePath)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Aqui se abre los resultados de menu
    ''' </summary>
    Private Sub INDgdvMenu_Click(sender As Object, e As EventArgs) Handles INDgdvMenu.Click
        Dim coord As System.Drawing.Point = Me.INDgdcMenu.PointToClient(MousePosition)
        Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvMenu.CalcHitInfo(coord)
        If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso (hit.HitField.FieldName.Trim().Equals("NameForm")) Then
            Me.OpenRegMenu()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se abre los resultados de registros y documentos
    ''' </summary>
    Private Sub INDgdvRegs_Click(sender As Object, e As EventArgs) Handles INDgdvRegs.Click, INDgdvDocs.Click
        Dim coord As System.Drawing.Point = MousePosition
        Select Case CType(sender, LayoutView).Name
            Case "INDgdvRegs"
                coord = Me.INDgdcRegs.PointToClient(coord)
                Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvRegs.CalcHitInfo(coord)
                If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso (hit.HitField.FieldName.Trim().Equals("Title")) Then
                    Me.OpenRegRegs()
                End If
            Case "INDgdvDocs"
                coord = Me.INDgdcDocs.PointToClient(coord)
                Dim hit As DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo = Me.INDgdvDocs.CalcHitInfo(coord)
                If hit IsNot Nothing AndAlso hit.HitField IsNot Nothing AndAlso hit.HitField.FieldName IsNot Nothing AndAlso (hit.HitField.FieldName.Trim().Equals("Title")) Then
                    Me.OpenRegDocs()
                End If
        End Select
    End Sub

#End Region

#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando se ejecuta la apertura de un formulario
    ''' </summary>
    Public Event QueryOpenForm(ByVal sender As Object, ByVal e As QueryOpenFormEventArgs)

#End Region

End Class

''' <summary>
''' Encapsula los datos del evento QueryOpenForm
''' </summary>
Public Class QueryOpenFormEventArgs
    Inherits EventArgs

#Region "Fields"

    ''' <summary>
    ''' Tag del frontal a abrir
    ''' </summary>
    Private _idForm As String
    ''' <summary>
    ''' Id de la entidad a abrir
    ''' </summary>
    Private _idEntity As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario a abrir
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property IdForm As String
        Get
            Return Me._idForm
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el id de la entidad
    ''' </summary>
    ''' <returns>Id de la entidad</returns>
    Public ReadOnly Property IdEntity As String
        Get
            Return Me._idEntity
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="idForm">Tag del formulario a abrir</param>
    ''' <param name="idEntity">Id de la entidad</param>
    Public Sub New(ByVal idForm As String, ByVal idEntity As String)
        Me._idForm = idForm
        Me._idEntity = idEntity
    End Sub

#End Region

End Class

''' <summary>
''' Agrega la capacidad de hacer scroll por pagina y no por tarjeta
''' </summary>
Friend Class MyLayoutViewScrollHelper

#Region "Fields"

    ''' <summary>
    ''' LayoutView a intervenir
    ''' </summary>
    Private _SelectedLayoutView As LayoutView
    ''' <summary>
    ''' Bandera para bloquear
    ''' </summary>
    Private locked As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el layoutview seleccionado
    ''' </summary>
    ''' <value>LayouView a intervenir</value>
    ''' <returns>LayouView intervenido</returns>
    Public Property SelectedLayoutView As LayoutView
        Get
            Return Me._SelectedLayoutView
        End Get
        Set(value As LayoutView)
            Me._SelectedLayoutView = value
            AddHandler Me._SelectedLayoutView.VisibleRecordIndexChanged, AddressOf SelectedLayoutView_VisibleRecordIndexChanged
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">LayoutView a intervenir</param>
    Public Sub New(ByVal view As LayoutView)
        Me._SelectedLayoutView = view
        AddHandler Me._SelectedLayoutView.VisibleRecordIndexChanged, AddressOf SelectedLayoutView_VisibleRecordIndexChanged
    End Sub

#End Region

#Region "Methods"

    Private Function GetVisibleCardsCount() As Int32
        Dim visibleCardsCount As Int32 = DirectCast(SelectedLayoutView.GetViewInfo(), LayoutViewInfo).VisibleCards.Count
        Return visibleCardsCount
    End Function

    Private Function GetPageByVisibleIndex(ByVal visibleIndex As Int32) As Int32
        Return visibleIndex / GetVisibleCardsCount()
    End Function

    Private Function IsScrollForward(ByVal e As LayoutViewVisibleRecordIndexChangedEventArgs) As Boolean
        If GetPageByVisibleIndex(e.VisibleRecordIndex) = GetPageByVisibleIndex(e.PrevVisibleRecordIndex) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetFirstCardIndex(ByVal pageIndex As Int32, ByVal isForward As Boolean) As Int32
        Dim delta As Int32 = 0
        If isForward Then
            delta = 1
        End If
        Return GetVisibleCardsCount() * (pageIndex + delta)
    End Function

    Private Sub ScrollPage(ByVal e As LayoutViewVisibleRecordIndexChangedEventArgs)
        SelectedLayoutView.VisibleRecordIndex = e.PrevVisibleRecordIndex
        SelectedLayoutView.VisibleRecordIndex = GetFirstCardIndex(GetPageByVisibleIndex(e.VisibleRecordIndex), IsScrollForward(e))
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Se captura el evento a ientervenir
    ''' </summary>
    Private Sub SelectedLayoutView_VisibleRecordIndexChanged(sender As Object, e As LayoutViewVisibleRecordIndexChangedEventArgs)
        If Not Me.locked Then
            Me.locked = True
            Me.ScrollPage(e)
            Me.locked = False
        End If
    End Sub

#End Region

End Class