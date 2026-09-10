#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.XtraLayout
Imports System.IO
Imports System.Text
Imports DevExpress.XtraEditors
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Threading.Tasks

#End Region

<ProvideProperty("IsCustomizable", GetType(LayoutControl))>
Public Class IndigoLayoutControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Fields"

    ''' <summary>
    ''' Separador de Layouts
    ''' </summary>
    Private Const LAYOUT_SEPARATOR As String = "|"

    ''' <summary>
    ''' Separador de grupos
    ''' </summary>
    Private Const GROUP_SEPARATOR As String = "#"

    ''' <summary>
    ''' Separador de propiedades
    ''' </summary>
    Private Const PROPERTY_SEPARATOR As String = ":"

    ''' <summary>
    ''' Constante con la cadena formateada de la ruta de la definicion
    ''' </summary>
    Private Const PATHDEF_FORMAT As String = "{0}\{1}\{2}\Customization\{3}"

    ''' <summary>
    ''' Constante con la extesión del archivo
    ''' </summary>
    Private Const EXT_FILE As String = ".def"

    ''' <summary>
    ''' Nombre del formulario
    ''' </summary>
    Private _formName As String

    ''' <summary>
    ''' Nombre de la compañia
    ''' </summary>
    Private _companyName As String

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    Private _productName As String

    ''' <summary>
    ''' Conjunto de controles que extienden sus propiedades
    ''' </summary>
    Private _hashTable As Hashtable

    ''' <summary>
    ''' Contenedor del control
    ''' </summary>
    Private _container As IContainer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene un valor que indica si el LayoutControl permite personalización
    ''' </summary>
    ''' <param name="obj">LayoutControl</param>
    ''' <returns>Un valor que indica si permite personalización</returns>
    <Browsable(False), Category("Indigo"), Description("Obtiene o asigna un valor que indica si el LayoutControl permite personalización"), DefaultValue(True)>
    Public Function GetIsCustomizable(ByVal obj As LayoutControl) As Boolean
        'Me.EnsureProperties(obj).IsCustomizable = obj.AllowCustomizationMenu
        Return Me.EnsureProperties(obj).IsCustomizable
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si el LayoutControl es personalizable
    ''' </summary>
    ''' <param name="obj">LayoutControl</param>
    ''' <param name="value">Valor a asignar</param>
    Public Sub SetIsCustomizable(ByVal obj As LayoutControl, ByVal value As Boolean)
        Me.EnsureProperties(obj).IsCustomizable = value
        'obj.AllowCustomizationMenu = value
    End Sub

    ''' <summary>
    ''' Encapsula las propiedades a extender
    ''' </summary>
    Private Class Properties

        ''' <summary>
        ''' Obtiene o asigna un valor que indica si el LayoutControl
        ''' permite personalización
        ''' </summary>
        ''' <value>Valor que indica si el LayoutControl es personalizable</value>
        ''' <returns>Un valor que indica si el LayoutControl es personalizable</returns>
        Public Property IsCustomizable As Boolean

    End Class

    ''' <summary>
    ''' Obtiene o asigna el nombre del formulario propietario del LayoutControl
    ''' </summary>
    ''' <value>Nombre del formulario propietario del LayoutControl</value>
    ''' <returns>El nombre del formulario propietario del LayoutControl</returns>
    <Category("Indigo"), Description("Obtiene o asigna el nombre del formulario propietario del LayoutControl")>
    Public Property FormName As String
        Get
            If Me._formName Is Nothing OrElse Me._formName.Trim().Equals(String.Empty) Then
                Me._formName = "FormBase" & DateTime.Now.ToString("yyyyMMdd")
            End If
            Return Me._formName
        End Get
        Set(value As String)
            If value IsNot Nothing AndAlso Me._formName IsNot Nothing AndAlso Not Me._formName.Trim().Equals(value.Trim()) Then
                Me._formName = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre de la compañia
    ''' </summary>
    ''' <value>Nombre de la compañia</value>
    ''' <returns>El nombre de la compañia</returns>
    <Category("Indigo"), Description("Obtiene o asigna el nombre de la compañia")>
    Public Property CompanyName As String
        Get
            Return Me._companyName
        End Get
        Set(value As String)
            If value IsNot Nothing AndAlso Me._companyName IsNot Nothing AndAlso Not Me._companyName.Trim().Equals(value.Trim()) Then
                Me._companyName = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre del producto
    ''' </summary>
    ''' <value>Nombre del producto</value>
    ''' <returns>El nombre del producto</returns>
    <Category("Indigo"), Description("Obtiene o asigna el nombre del producto")>
    Public Property ProductName As String
        Get
            Return Me._productName
        End Get
        Set(value As String)
            If value IsNot Nothing AndAlso Me._productName IsNot Nothing AndAlso Not Me._productName.Trim().Equals(value.Trim()) Then
                Me._productName = value.Trim()
            End If
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._hashTable = New Hashtable()
        Me._formName = "FormBase" & DateTime.Now.ToString("yyyyMMdd")
        Me._companyName = Infrastructure.CrossCutting.Base.Utils.GetCompanyName()
        Me._productName = Infrastructure.CrossCutting.Base.Utils.GetAppName()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="container">Contenedor del control</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        container.Add(Me)
    End Sub

#End Region

#Region "Delegates"

    ''' <summary>
    ''' Delegado al método de agregar controles a LayoutControl
    ''' </summary>
    ''' <param name="ly">LayoutControl al que se va a agregar el control</param>
    ''' <param name="ctr">Control a agregar</param>
    Private Delegate Sub AddControlDelegate(ByVal ly As LayoutControl, ByVal ctr As Control)

    ''' <summary>
    ''' Delegado al método de restaurar definicion de LayoutControl
    ''' </summary>
    ''' <param name="ly">LayoutControl que va a restaurar la definicion</param>
    ''' <param name="st">Stream de la definicion</param>
    Private Delegate Sub RestoreLayoutDelegate(ByVal ly As LayoutControl, ByVal st As Stream)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los componentes
    ''' </summary>
    Private Sub InitializeComponent()
        Me._container = New System.ComponentModel.Container()
    End Sub

    ''' <summary>
    ''' Asegura que el control está registrado, de lo contrario
    ''' lo registra en la tabla
    ''' </summary>
    ''' <param name="obj">Control a buscar</param>
    ''' <returns>Propiedades extendidas del control</returns>
    Private Function EnsureProperties(ByVal obj As Object) As Properties
        Dim p As Properties = DirectCast(Me._hashTable(obj), Properties)
        If p Is Nothing Then
            p = New Properties()
            p.IsCustomizable = DirectCast(obj, LayoutControl).AllowCustomizationMenu
            Me._hashTable(obj) = p
            DirectCast(obj, LayoutControl).RegisterCustomPropertyGridWrapper(GetType(LayoutControlItem), GetType(LayoutControlItemPropertyGridWrapper))
            DirectCast(obj, LayoutControl).RegisterUserCustomizationForm(GetType(ToolsCustomizationForm))
            AddHandler DirectCast(obj, LayoutControl).HideCustomization, AddressOf LayoutControl_HideCustomization
        End If
        Return p
    End Function

    ''' <summary>
    ''' Verifica si la ruta existe, sino lo crea
    ''' </summary>
    ''' <param name="path">Ruta a comprobar</param>
    Private Sub EnsurePathExists(ByVal path As String)
        If Not My.Computer.FileSystem.DirectoryExists(path) Then
            My.Computer.FileSystem.CreateDirectory(path)
        End If
    End Sub

    ''' <summary>
    ''' Agrega un control a un LayoutControl
    ''' </summary>
    ''' <param name="ly">LayoutControl al que se va a agregar el control</param>
    ''' <param name="ctr">Control a agregar</param>
    Private Sub AddControl(ByVal ly As LayoutControl, ByVal ctr As Control)
        If ly.InvokeRequired Then
            Dim dl As New AddControlDelegate(AddressOf AddControl)
            ly.Invoke(dl, New Object() {ly, ctr})
        Else
            ly.Controls.Add(ctr)
        End If
    End Sub

    ''' <summary>
    ''' Restaura una definicion en un LayoutControl
    ''' </summary>
    ''' <param name="ly">LayoutControl que va a restaurar la definicion</param>
    ''' <param name="st">Stream de la definicion</param>
    Private Sub RestoreLayout(ByVal ly As LayoutControl, ByVal st As Stream)
        If ly.InvokeRequired Then
            Dim dl As New RestoreLayoutDelegate(AddressOf RestoreLayout)
            ly.Invoke(dl, New Object() {ly, st})
        Else
            ly.RestoreLayoutFromStream(st)
        End If
    End Sub

    ''' <summary>
    ''' Carga la definición de cada uno de los LayoutControl
    ''' </summary>
    Public Sub LoadDefinition()
        Dim pathDefinition As String = String.Format(PATHDEF_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Me._companyName, Me._productName, Me._formName)
        pathDefinition = Path.Combine(pathDefinition, Me._formName & EXT_FILE)
        If File.Exists(pathDefinition) Then
            Dim strFile As String = File.ReadAllText(pathDefinition)
            'Obtenemos todos los grupos de LayoutControl
            Dim aLayouts() As String = strFile.Split(LAYOUT_SEPARATOR)
            If aLayouts IsNot Nothing AndAlso aLayouts.Length = Me._hashTable.Count Then
                Dim index As Int32 = 0
                For Each de As DictionaryEntry In Me._hashTable
                    Dim p As LayoutControl = TryCast(de.Key, LayoutControl)
                    'Obtenemos los sub grupos que componen el LayoutControl
                    Dim aGroups() As String = aLayouts(index).Split(GROUP_SEPARATOR)
                    If aGroups IsNot Nothing AndAlso aGroups.Length = 3 Then
                        Dim streamDefinition As Stream = CustomHelper.Base64StringToStream(aGroups(0))
                        Dim aAllowHide() As String = Encoding.UTF8.GetString(Convert.FromBase64String(aGroups(1))).Split(PROPERTY_SEPARATOR)
                        Dim aControls() As String = Encoding.UTF8.GetString(Convert.FromBase64String(aGroups(2))).Split(GROUP_SEPARATOR)
                        'Recorremos los contoles creandolos y asignandolos al LayoutControl
                        '                For Each c As String In aControls
                        '                    Dim aProperties() As String = c.Split(PROPERTY_SEPARATOR)
                        '                    If aProperties IsNot Nothing AndAlso aProperties.Length = 7 Then
                        '                        'Creamos el control
                        '                        Dim t As Type = CustomHelper.ListCustomControls(True)(aProperties(0))
                        '                        Dim ctr As Control = Nothing
                        '                        If t IsNot Nothing Then
                        '                            ctr = Activator.CreateInstance(t)
                        '                        End If
                        '                        If ctr IsNot Nothing Then
                        '                            ctr.Name = aProperties(1)
                        '                            ctr.Font = New Font(aProperties(2).Split(",")(0).Trim(), Convert.ToSingle(aProperties(2).Split(",")(1).Trim().Replace("pt", "")))
                        '                            ctr.ForeColor = Color.FromArgb(Convert.ToInt32(aProperties(3)))
                        '                            CTypeDynamic(ctr, ctr.GetType()).FieldType = [Enum].Parse(GetType(TypeCode), aProperties(4))
                        '                            CTypeDynamic(ctr, ctr.GetType()).FieldLength = Convert.ToInt64(aProperties(5))
                        '                            CTypeDynamic(ctr, ctr.GetType()).FieldName = aProperties(6)
                        '                            'Asignamos el control al LayoutControl
                        '                            Me.AddControl(p, ctr)
                        '                        End If
                        '                    End If
                        '                Next
                        '                'Asignamos la definición al LayoutControl
                        '                Me.RestoreLayout(p, streamDefinition)
                        '                'Recorremos los LayoutControlItem que son obligatorios
                        '                For Each al As String In aAllowHide
                        '                    If Not al.Trim().Equals(String.Empty) Then
                        '                        p.Items.FindByName(al).AllowHide = False
                        '                    End If
                        '                Next
                    End If
                    index += 1
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga asíncronamente la definición de cada uno de los LayoutControl
    ''' </summary>
    Public Function LoadDefinitionAsync() As Task
        Return Task.Factory.StartNew(AddressOf LoadDefinition)
    End Function

    ''' <summary>
    ''' Asigna valor a la propiedad AllowHide en cada uno de los LayoutControlItems
    ''' del LayoutControlGroup
    ''' </summary>
    ''' <param name="layoutGroup">LayoutControlGroup a modificar</param>
    ''' <param name="value">Valor a asignar en la propiedad</param>
    Public Sub SetAllowHideInLayoutControlGroup(ByVal layoutGroup As LayoutControlGroup, ByVal value As Boolean)
        For Each item As BaseLayoutItem In (From i As BaseLayoutItem In layoutGroup.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
            Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
            it.AllowHide = value
        Next
    End Sub

    ''' <summary>
    ''' Valida todos los campos requeridos en la
    ''' colección de LayoutControl
    ''' </summary>
    ''' <returns>Una lista de los campos sin diligenciar</returns>
    Public Function ValidateFields() As ValidateResult
        Dim result As New ValidateResult()
        Dim refCtr As Control = Nothing
        For Each de As DictionaryEntry In Me._hashTable
            Dim layoutControl As LayoutControl = TryCast(de.Key, LayoutControl)
            'Recorremos los LayoutControlItem
            For Each item As BaseLayoutItem In (From i As BaseLayoutItem In layoutControl.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
                Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
                If TypeOf it.Control Is IValidable OrElse Me.GetIsBaseEditBaseType(it.Control.GetType()) Then
                    Dim ctr As Object = it.Control
                    If Not it.ShowInCustomizationForm OrElse Not it.AllowHide Then
                        If ctr.EditValue IsNot Nothing Then
                            If ctr.EditValue.GetType().Equals(GetType(String)) AndAlso ctr.EditValue.ToString().Trim().Equals(String.Empty) Then
                                If result.EmptyFieldNames.Count = 0 Then
                                    refCtr = ctr
                                End If
                                result.EmptyFieldNames.Add(it.Text)
                            End If
                        Else
                            If result.EmptyFieldNames.Count = 0 Then
                                refCtr = ctr
                            End If
                            result.EmptyFieldNames.Add(it.Text)
                        End If
                    End If
                End If
            Next
        Next
        If refCtr IsNot Nothing Then
            refCtr.Focus()
        End If
        result.ResultStatus = (result.EmptyFieldNames.Count = 0)
        Return result
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el tipo pasado tiene como base
    ''' un control BaseEdit
    ''' </summary>
    ''' <param name="type">Tipo pasado a consultar</param>
    Private Function GetIsBaseEditBaseType(ByVal type As Type) As Boolean
        Dim result As Boolean = False
        IsBaseEditBaseType(type, result)
        Return result
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el tipo pasado tiene como base
    ''' un control BaseEdit
    ''' </summary>
    ''' <param name="type">Tipo pasado a consultar</param>
    ''' <param name="result">Resultado de la busqueda</param>
    Private Sub IsBaseEditBaseType(ByVal type As Type, ByRef result As Boolean)
        If type.BaseType.Equals(GetType(DevExpress.XtraEditors.BaseEdit)) Then
            result = True
        Else
            If Not type.BaseType.Equals(GetType(Object)) Then
                IsBaseEditBaseType(type.BaseType, result)
            Else
                result = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el conjunto de valores en los campos personalizados
    ''' </summary>
    ''' <returns>Conjunto de valores en los campos personalizados</returns>
    Public Function GetCustomFieldsValue() As DataSet
        Return New DataSet()
    End Function

    ''' <summary>
    ''' Asigna un conjunto de valores a los campos personalizados
    ''' </summary>
    ''' <param name="data">Conjunto de valores a asignar</param>
    Public Sub SetCustomFieldsValue(ByVal data As DataSet)

    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se realiza la lógica para guardar las definiciones y controles personalizados
    ''' en un solo archivo
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub LayoutControl_HideCustomization(sender As Object, e As EventArgs)
        If DirectCast(sender, LayoutControl).IsModified Then
            Dim pathDefinition As String = String.Format(PATHDEF_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Me._companyName, Me._productName, Me._formName)
            'Nos aseguramos que la ruta exista
            Me.EnsurePathExists(pathDefinition)
            Dim strResult As New StringBuilder()
            For Each de As DictionaryEntry In Me._hashTable
                Dim p As LayoutControl = TryCast(de.Key, LayoutControl)
                Dim streamLayout As New MemoryStream()
                p.SaveLayoutToStream(streamLayout)
                'Si NO es el primer LayoutControl
                If Not strResult.ToString().Equals(String.Empty) Then
                    strResult.Append(LAYOUT_SEPARATOR)
                End If
                strResult.Append(CustomHelper.StreamToBase64String(streamLayout))
                If Me.EnsureProperties(p).IsCustomizable Then
                    'Recorremos los LayoutControlItem
                    For Each item As BaseLayoutItem In (From i As BaseLayoutItem In p.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
                        Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)

                        strResult.Append(GROUP_SEPARATOR)
                        'Persistimos el LayoutControlItem
                        strResult.Append(CustomHelper.Serialize(it))
                        'Persistimos el control del LayoutControlItem
                        If it.Control IsNot Nothing Then
                            strResult.Append(GROUP_SEPARATOR)
                            strResult.Append(it.Control.Name & PROPERTY_SEPARATOR & CustomHelper.Serialize(it.Control))
                        End If
                    Next
                End If
            Next
            If File.Exists(Path.Combine(pathDefinition, Me._formName & EXT_FILE)) Then
                File.Delete(Path.Combine(pathDefinition, Me._formName & EXT_FILE))
            End If
            File.AppendAllText(Path.Combine(pathDefinition, Me._formName & EXT_FILE), strResult.ToString())
        End If
    End Sub

#End Region

#Region "IExtenderProvider"

    Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        Return extendee.GetType().Equals(GetType(LayoutControl))
    End Function

#End Region

#Region "ISupportInitialize"

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula el resultado de la validación de campos
''' </summary>
Public Class ValidateResult

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si la validación de campos fue exitosa
    ''' </summary>
    ''' <value>Valor que indica si la validación de capos fue exitosa</value>
    ''' <returns>Un valor que indica si la validación de capos fue exitosa</returns>
    Public Property ResultStatus As Boolean

    ''' <summary>
    ''' Obtiene o asigna la lista de nombres de campos vacios o nulos
    ''' </summary>
    ''' <value>Lista de campos vacios o nulos</value>
    ''' <returns>La lista de campos vacios o nulos</returns>
    Public Property EmptyFieldNames As List(Of String)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.New(False, New List(Of String))
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="resultStatus">Valor que indica si la validación de campos fue exitosa</param>
    ''' <param name="emptyFieldNames">Lista de nombres de campos vacios o nulos</param>
    Public Sub New(ByVal resultStatus As Boolean, ByVal emptyFieldNames As List(Of String))
        Me.EmptyFieldNames = emptyFieldNames
        Me.ResultStatus = resultStatus
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Retorna una sola cadena de caracteres con los nombres concatenados por un salto de linea
    ''' </summary>
    ''' <returns>Nombres concatenados por salto de linea</returns>
    Public Overrides Function ToString() As String
        If Me.EmptyFieldNames.Count > 0 Then
            If Me.EmptyFieldNames.Count > 1 Then
                Return String.Join(vbCrLf, Me.EmptyFieldNames.ToArray())
            Else
                Return Me.EmptyFieldNames(0)
            End If
        Else
            Return MyBase.ToString()
        End If
    End Function

#End Region

End Class