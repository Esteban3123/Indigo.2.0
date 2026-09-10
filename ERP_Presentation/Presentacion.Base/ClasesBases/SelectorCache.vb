''' <summary>
''' Clase genérica para gestionar selecciones múltiples en controles DevExpress con GridView/GridControl.
''' 
''' Actúa como un caché en memoria que almacena filas seleccionadas por el usuario permitiendo:
''' - Recuperar claves (IDs) para filtros SQL: "1,2,3,4"
''' - Obtener valores (códigos, nombres) para mostrar al usuario: "COD001,COD002,COD003"
''' - Persistir selecciones mientras el control está cerrado
''' - Implementar selección múltiple tipo checkbox en grillas
''' 
''' CASOS DE USO COMUNES:
''' - Selección múltiple en SearchLookUpEdit o PopupContainerEdit
''' - Filtros dinámicos en reportes, consultas, exportaciones
''' - Búsquedas avanzadas con múltiples criterios seleccionables
''' - Cualquier escenario que requiera mantener varias filas seleccionadas
''' </summary>
Public Class SelectorCache

#Region "Builder"

    ''' <summary>
    ''' Nombre del campo que actúa como clave única (generalmente "Id", "Codigo", etc.)
    ''' </summary>
    Private ReadOnly _KeyFieldName As String

    ''' <summary>
    ''' Nombres de los campos que se desean almacenar como valores (Ej: "Code", "Name", "Description")
    ''' </summary>
    Private ReadOnly _ValueFieldNames As String()

    ''' <summary>
    ''' Diccionario que almacena los elementos seleccionados.
    ''' - Clave externa: El valor del campo clave (Id, Código, etc.)
    ''' - Valor: Diccionario interno con los campos especificados en _ValueFieldNames
    ''' </summary>
    Private _valuesCache As New Dictionary(Of Object, Dictionary(Of String, Object))()

    ''' <summary>
    ''' Constructor de la clase SelectorCache.
    ''' </summary>
    ''' <param name="keyFieldName">Campo que actúa como clave única (ID, PrimaryKey, Código único, etc).</param>
    ''' <param name="valueFieldNames">Uno o más campos para almacenar/mostrar (Código, Nombre, Descripción, etc).</param>
    ''' <remarks>
    ''' EJEMPLOS DE USO:
    ''' - New SelectorCache("Id", "Code") → Almacena Id, muestra Code
    ''' - New SelectorCache("ProductId", "ProductCode", "ProductName") → Almacena ProductId, puede acceder a Code y Name
    ''' - New SelectorCache("CustomerId", "CustomerCode") → Almacena CustomerId, muestra CustomerCode
    ''' 
    ''' IMPORTANTE:
    ''' - El primer valueFieldName se usa en ToString() para mostrar al usuario
    ''' - Todos los valueFieldNames se almacenan y pueden recuperarse con GetValueByKey()
    ''' - keyFieldName DEBE ser único (Id, PrimaryKey, GUID, Código único)
    ''' </remarks>
    Public Sub New(ByVal keyFieldName As String, ByVal ParamArray valueFieldNames As String())
        _KeyFieldName = keyFieldName
        _ValueFieldNames = valueFieldNames
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia todas las selecciones del caché. Útil al cambiar contexto, limpiar filtros o recargar datos.
    ''' Después: control.Properties.NullText = String.Empty
    ''' </summary>
    Public Sub Clear()
        _valuesCache.Clear()
    End Sub

    ''' <summary>
    ''' Diccionario interno con todas las selecciones. USO AVANZADO, normalmente usar GetKeys()/ToString().
    ''' Estructura: Dictionary(KeyValue, Dictionary(FieldName, FieldValue))
    ''' </summary>
    Public ReadOnly Property ValuesCache() As Dictionary(Of Object, Dictionary(Of String, Object))
        Get
            Return _valuesCache
        End Get
    End Property

    ''' <summary>
    ''' Agrega/quita una fila del caché (modo toggle). Si NO está seleccionada → agrega, si YA está → elimina.
    ''' 
    ''' USO TÍPICO: En evento RowCellClick cuando usuario hace clic en checkbox
    ''' Ejemplo: _selector.SetValue(gridView.GetRow(e.RowHandle))
    ''' Luego refrescar vista: gridView.RefreshData()
    ''' </summary>
    ''' <param name="row">Fila de datos. Soporta: Entidades XPO, Tuples, POCOs, Proxies DevExpress.</param>
    ''' <param name="selection">Nothing=toggle automático, True=forzar agregar, False=forzar quitar.</param>
    Public Sub SetValue(ByVal row As Object, Optional selection As Boolean? = Nothing)
        Dim key = GetKeyByRow(row)
        If ValidateExistsKey(key) Then
            ' Ya existe en el caché
            If selection IsNot Nothing AndAlso selection Then
                ' Se quiere agregar pero ya existe → No hacer nada
                Exit Sub
            End If
            ' Modo toggle o selection=False → Eliminar
            RemoveRowByKey(key)
        Else
            ' No existe en el caché
            If selection IsNot Nothing AndAlso Not selection Then
                ' Se quiere quitar pero no existe → No hacer nada
                Exit Sub
            End If
            ' Modo toggle o selection=True → Agregar
            AddRowByKey(key, row)
        End If
    End Sub

    ''' <summary>
    ''' Verifica si una fila está seleccionada (devuelve Object: True/False).
    ''' OBSOLETO: Usar ValidateExistsRow() en su lugar. Se mantiene por compatibilidad.
    ''' </summary>
    ''' <param name="row">Fila a verificar.</param>
    ''' <returns>True si está seleccionada, False si no.</returns>
    Public Function GetValue(ByVal row As Object) As Object
        Return ValidateExistsKey(GetKeyByRow(row))
    End Function

    ''' <summary>
    ''' Elimina una fila del caché (solo eliminar, no hace toggle como SetValue).
    ''' No hace nada si la fila no existe. Luego actualizar: control.Properties.NullText = _selector.ToString()
    ''' </summary>
    ''' <param name="row">Fila a eliminar.</param>
    Public Sub UnSetValue(ByVal row As Object)
        Dim key = GetKeyByRow(row)
        If ValidateExistsKey(key) Then
            RemoveRowByKey(key)
        End If
    End Sub

    ''' <summary>
    ''' Elimina elemento del caché usando solo su clave, sin necesitar el objeto fila completo.
    ''' 
    ''' Útil cuando conoces el ID pero no tienes acceso al objeto completo.
    ''' Ejemplo: _selector.UnSetValueByKey(5) → elimina elemento con Id=5
    ''' </summary>
    ''' <param name="key">Clave del elemento a eliminar (Id, PrimaryKey, Código único, etc).</param>
    Public Sub UnSetValueByKey(ByVal key As Object)
        If ValidateExistsKey(key) Then
            RemoveRowByKey(key)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el valor de un campo específico de un elemento seleccionado usando su clave.
    ''' </summary>
    ''' <param name="key">Clave del elemento (Id del elemento en caché).</param>
    ''' <param name="fieldName">Campo a obtener. Si es Nothing/Empty: devuelve primer campo del constructor.</param>
    ''' <returns>Valor del campo, o Nothing si no existe la clave o el campo.</returns>
    ''' <remarks>
    ''' CASOS DE USO:
    ''' - Obtener nombre de un producto: GetValueByKey(5, "ProductName")
    ''' - Obtener descripción: GetValueByKey(10, "Description")
    ''' - Obtener primer campo: GetValueByKey(7) → devuelve valor del primer campo
    ''' 
    ''' EJEMPLO PRÁCTICO:
    ''' ' Constructor: New SelectorCache("ProductId", "ProductCode", "ProductName", "Price")
    ''' Dim code = _selector.GetValueByKey(5, "ProductCode") → "PROD005"
    ''' Dim name = _selector.GetValueByKey(5, "ProductName") → "Laptop Dell"
    ''' Dim price = _selector.GetValueByKey(5, "Price") → 1500.00
    ''' Dim defaultField = _selector.GetValueByKey(5) → "PROD005" (primer campo)
    ''' 
    ''' VALIDACIONES:
    ''' - Si la clave no existe → Nothing
    ''' - Si el campo no fue configurado en constructor → Nothing
    ''' - Si no hay campos configurados → Nothing
    ''' </remarks>
    Public Function GetValueByKey(ByVal key As Object, Optional ByVal fieldName As String = Nothing) As Object
        ' Validar que haya campos configurados
        If _ValueFieldNames Is Nothing OrElse _ValueFieldNames.Count = 0 Then
            Return Nothing
        End If

        ' Validar que la clave existe
        If Not ValidateExistsKey(key) Then
            Return Nothing
        End If

        ' Si no se especifica campo, usar el primero
        If String.IsNullOrEmpty(fieldName) Then
            fieldName = _ValueFieldNames.FirstOrDefault()
        End If

        ' Validar que el campo solicitado existe en la configuración
        If Not _ValueFieldNames.Contains(fieldName) Then
            Return Nothing
        End If

        Return Me._valuesCache(key)(fieldName)
    End Function

    ''' <summary>
    ''' Verifica si una fila está seleccionada. Devuelve True/False para pintar checkboxes.
    ''' 
    ''' USO PRINCIPAL: En CustomUnboundColumnData para indicar estado de checkbox
    ''' Ejemplo: If e.Column.FieldName = "Selected" Then e.Value = _selector.ValidateExistsRow(e.Row)
    ''' </summary>
    ''' <param name="row">Fila a verificar. Puede ser XPO, Tuple, POCO, Proxy DevExpress.</param>
    ''' <returns>True si la fila está en caché (seleccionada), False en caso contrario.</returns>
    Public Function ValidateExistsRow(ByVal row As Object) As Object
        Return ValidateExistsKey(GetKeyByRow(row))
    End Function

    ''' <summary>
    ''' Obtiene la clave del elemento en la posición especificada (índice 0-based).
    ''' POCO USADO: Preferir GetKeys() o iterar ValuesCache. Orden NO garantizado.
    ''' </summary>
    ''' <param name="index">Índice de la posición (0-based).</param>
    ''' <returns>Clave del elemento o Nothing si índice fuera de rango.</returns>
    Public Function GetKey(index As Integer) As Object
        If index >= 0 AndAlso index < Me.Count() Then
            Return _valuesCache.Keys(index)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene las claves (IDs) seleccionadas separadas por comas para usar en filtros SQL.
    ''' 
    ''' RETORNA: "1,2,5,8" → Listo para usar en WHERE Campo IN (...)
    ''' USO: filter &amp;= $"CampoId IN ({_selector.GetKeys()})"
    ''' </summary>
    ''' <returns>String con claves separadas por comas. String.Empty si no hay selecciones.</returns>
    Public Function GetKeys() As String
        Return String.Format("{0}", String.Join(",", _valuesCache.Keys.Distinct()))
    End Function

    ''' <summary>
    ''' Obtiene array con las claves seleccionadas.
    ''' Menos usado que GetKeys(). Útil para: LINQ Contains(), APIs que aceptan arrays.
    ''' </summary>
    ''' <returns>Array Object[] con las claves (Integer[], String[], etc según el tipo de clave).</returns>
    Public Function GetKeysToArray() As Object()
        Return _valuesCache.Keys.ToArray()
    End Function

    ''' <summary>
    ''' Obtiene los CÓDIGOS/VALORES (no IDs) para mostrar al usuario en el control.
    ''' 
    ''' RETORNA: "COD001,COD002,COD003" → Texto legible usando el PRIMER campo del constructor
    ''' USO: control.Properties.NullText = _selector.ToString()
    ''' </summary>
    ''' <returns>String con valores del primer campo separados por comas. Nothing si no hay selecciones o campos.</returns>
    ''' <remarks>
    ''' COMPORTAMIENTO: Siempre usa el PRIMER campo especificado en el constructor.
    ''' EJEMPLOS SEGÚN CONSTRUCTOR:
    ''' - New SelectorCache("Id", "Code") → ToString() = "COD001,COD002,COD003"
    ''' - New SelectorCache("Id", "Name") → ToString() = "Juan Pérez,María López,..."
    ''' - New SelectorCache("Id", "Code", "Name") → ToString() = "COD001,COD002" (usa Code, ignora Name)
    ''' NOTA: Para acceder a otros campos usa GetValueByKey(id, "FieldName")
    ''' </remarks>
    Public Overrides Function ToString() As String
        If _ValueFieldNames Is Nothing OrElse _ValueFieldNames.Count = 0 Then
            Return Nothing
        End If

        Dim fieldName = _ValueFieldNames.FirstOrDefault()
        Return String.Format("{0}", String.Join(",", _valuesCache.Values.ToList().Select(Function(d) d(fieldName))))
    End Function

    ''' <summary>
    ''' Obtiene el número total de elementos seleccionados.
    ''' Uso: If _selector.Count() > 0 Then ... aplicar filtro
    ''' </summary>
    ''' <returns>Cantidad de elementos seleccionados (0 si no hay ninguno).</returns>
    Public Function Count() As Integer
        Return _valuesCache.Keys.Count
    End Function

#Region "Private Methods"

    ''' <summary>
    ''' Método interno CRÍTICO que extrae valores de diferentes tipos de objetos mediante reflexión.
    ''' 
    ''' Soporta múltiples tipos de fuentes de datos que DevExpress puede usar:
    ''' - Entidades XPO (XPLiteObject y derivados)
    ''' - Tuples (proyecciones y datos anónimos)
    ''' - POCOs (Plain Old CLR Objects)
    ''' - Proxies thread-safe de DevExpress (XPInstantFeedbackSource asíncrono)
    ''' </summary>
    ''' <param name="row">Objeto fila de cualquier tipo soportado.</param>
    ''' <param name="fieldName">Nombre del campo/propiedad a extraer.</param>
    ''' <returns>Valor del campo o Nothing si no se puede extraer.</returns>
    Private Function GetDataByRowAndField(ByVal row As Object, ByVal fieldName As String) As Object
        ' Validar que la fila no sea Nothing y no sea un objeto "NotLoaded" de DevExpress
        If row IsNot Nothing AndAlso row.GetType <> GetType(DevExpress.Data.NotLoadedObject) Then

            ' CASO 1: Tuplas (datos proyectados)
            If row.GetType().FullName.StartsWith("System.Tuple") Then
                Dim propInf = row.GetType().GetProperty(fieldName)
                If propInf IsNot Nothing Then
                    Dim val = propInf.GetValue(row)
                    Return val
                End If

                ' CASO 2: Entidades XPO (XPLiteObject)
            ElseIf row.GetType()?.BaseType?.FullName?.EndsWith("XPLiteObject") Then
                Dim propInf = row.GetType().GetProperty(fieldName)
                If propInf IsNot Nothing Then
                    Dim val = propInf.GetValue(row)
                    Return val
                End If

                ' CASO 3: Otros tipos (POCO, ViewModels, DTOs)
            Else
                ' Primero intentar acceso directo al objeto
                Dim propInf = row.GetType().GetProperty(fieldName)
                If propInf IsNot Nothing Then
                    Dim val = propInf.GetValue(row)
                    Return val
                End If

                ' CASO 4: Proxy thread-safe de DevExpress (XPInstantFeedbackSource asíncrono)
                ' Si no funcionó el acceso directo, verificar si es un proxy y desenvolverlo
                If TypeOf row Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                    ' Obtener la entidad original del proxy
                    Dim entity = CType(row, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                    propInf = entity.GetType().GetProperty(fieldName)
                    If propInf IsNot Nothing Then
                        Dim val = propInf.GetValue(entity)
                        Return val
                    End If
                End If
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Extrae el valor del campo clave de una fila.
    ''' </summary>
    ''' <param name="row">Objeto fila del cual extraer la clave.</param>
    ''' <returns>El valor de la clave (Id, Código, etc.) o Nothing si no se pudo extraer.</returns>
    Private Function GetKeyByRow(ByVal row As Object) As Object
        Return GetDataByRowAndField(row, _KeyFieldName)
    End Function

    ''' <summary>
    ''' Extrae el valor de un campo específico de una fila.
    ''' </summary>
    ''' <param name="row">Objeto fila del cual extraer el valor.</param>
    ''' <param name="fieldName">Nombre del campo a extraer.</param>
    ''' <returns>El valor del campo o Nothing si no se pudo extraer.</returns>
    Private Function GetValueByFieldName(ByVal row As Object, ByVal fieldName As String) As Object
        Return GetDataByRowAndField(row, fieldName)
    End Function

    ''' <summary>
    ''' Verifica si una clave específica existe en el diccionario de selecciones.
    ''' </summary>
    ''' <param name="key">Clave a verificar (Id, Código, etc.).</param>
    ''' <returns>True si la clave existe, False en caso contrario.</returns>
    Private Function ValidateExistsKey(ByVal key As Object) As Boolean
        If key IsNot Nothing AndAlso _valuesCache.Any() Then
            Return _valuesCache.ContainsKey(key)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Agrega una fila al caché extrayendo y almacenando todos los campos configurados en el constructor.
    ''' </summary>
    ''' <param name="key">Clave única del elemento (previamente extraída de la fila).</param>
    ''' <param name="row">Objeto fila completo del cual extraer los valores.</param>
    Private Sub AddRowByKey(ByVal key As Object, ByVal row As Object)
        ' Crear diccionario interno para almacenar los valores de los campos
        Dim dictionary = New Dictionary(Of String, Object)

        ' Extraer todos los campos configurados de la fila
        For Each fieldName In _ValueFieldNames
            Dim value = GetValueByFieldName(row, fieldName)
            dictionary(fieldName) = value
        Next

        ' Validar que la clave no sea Nothing antes de agregar
        If key Is Nothing Then
            Exit Sub
        End If

        ' Agregar o actualizar la entrada en el caché
        _valuesCache(key) = dictionary
    End Sub

    ''' <summary>
    ''' Elimina una entrada del diccionario de selecciones usando su clave.
    ''' </summary>
    ''' <param name="key">Clave del elemento a eliminar.</param>
    Private Sub RemoveRowByKey(ByVal key As Object)
        _valuesCache.Remove(key)
    End Sub

#End Region

#End Region

End Class
