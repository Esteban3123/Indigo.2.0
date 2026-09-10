#Region "Imports"

Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports Autofac.Util
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Practices.Unity

#End Region

''' <summary>
''' Provee metodos de extensiòn
''' </summary>
Public Module Extentions

#Region "Date"
    Private ReadOnly _jan1St1970 As New Date(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)

    <Extension()>
    Public Function ToMilliseconds(ByVal value As Date) As Long
        Return (value - _jan1St1970).TotalMilliseconds
    End Function
#End Region

#Region "ArrayList"

    ''' <summary>
    ''' Convierte el arreglo a un tipo especifico de entidad
    ''' </summary>
    ''' <typeparam name="TEntity">Tipo de entidad a convertir</typeparam>
    ''' <param name="arrayList">Arreglo que extiende el metodo</param>
    ''' <returns>Lista de entidades</returns>
    <Extension()>
    Public Function ToEntityList(Of TEntity)(ByVal arrayList As IList) As List(Of TEntity)
        Dim list As New List(Of TEntity)(arrayList.Count)
        For Each instance As TEntity In arrayList
            list.Add(instance)
        Next
        Return list
    End Function

#End Region

#Region "String"

    ''' <summary>
    ''' Convierte la cadena a base 64
    ''' </summary>
    ''' <param name="str">Cadena a convertir</param>
    ''' <returns>Cadena en base 64</returns>
    <Extension()>
    Public Function ToBase64(ByVal str As String) As String
        If str IsNot Nothing AndAlso Not str.Trim().Equals(String.Empty) Then
            Return Convert.ToBase64String(Text.UTF32Encoding.UTF32.GetBytes(str))
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Codifica los caracteres no soportados por el formato Xml
    ''' </summary>
    ''' <param name="str">Cadena a convertir</param>
    ''' <returns>Cadena resultado</returns>
    <Extension()>
    Public Function ConvertToXmlText(ByVal str As String) As String
        If str IsNot Nothing AndAlso Not str.Trim().Equals(String.Empty) Then
            Return str.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;")
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Limpia la cadena de caracteres especiales
    ''' </summary>
    ''' <param name="str">Cadena a limpiar</param>
    ''' <param name="exceptions">arreglo de caracteres que seran una excepción al momento de limpiar la cadena</param>
    ''' <returns>Cadena resultado</returns>
    <Extension()>
    Public Function CleanSpecialChars(ByVal str As String, Optional ByVal exceptions As String = "") As String
        'str = str.Normalize(Text.NormalizationForm.FormD)
        'Dim reg As New Regex("[^a-zA-Z0-9 ]")
        'Return reg.Replace(str, "")

        '''''''''''''''''''' NO CAMBIAR ESTA FUNCION YA QUE SI LA CAMBIAN PODRIAN GENERAR ERRORES EN DISPENSACION FARMACEUTICA O EN LAS PARTES EN LAS QUE SE USE '''''''''''''
        Dim validChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 _-" & exceptions
        Dim strRes As String = String.Empty
        For Each c As Char In str

            If validChars.Contains(c) Then
                strRes &= c.ToString()
            Else
                strRes &= ChangeCharacters(c)
            End If
        Next
        Return strRes
    End Function

    ''' <summary>
    ''' Funcion para cambiar caracteres especiales para la generacion de archivo plano dispersion de fondos
    ''' </summary>
    ''' <param name="str">Cadena de caracteres</param>
    ''' <param name="ExtDictionary">Excepciones Adicionales</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ChangeCharacters(ByVal str As String, Optional ByVal ExtDictionary As Dictionary(Of String, String) = Nothing)
        Dim strRes As String = String.Empty

        Dim dictionary = New Dictionary(Of String, String) From {{"Ñ", "N"}, {"&", "Y"}, {"ü", "u"}, {"é", "e"},
                                                                {"â", "a"}, {"ä", "a"}, {"à", "a"}, {"å", "a"},
                                                                {"ê", "e"}, {"ë", "e"}, {"è", "e"}, {"ï", "i"},
                                                                {"î", "i"}, {"ì", "i"}, {"Ä", "A"}, {"Å", "A"},
                                                                {"É", "E"}, {"ô", "o"}, {"ö", "o"}, {"ò", "o"},
                                                                {"û", "u"}, {"ù", "u"}, {"ÿ", "Y"}, {"Ö", "O"},
                                                                {"Ü", "U"}, {"á", "a"}, {"í", "i"}, {"ó", "o"},
                                                                {"ú", "u"}, {"ñ", "n"}, {"Á", "A"}, {"Â", "A"},
                                                                {"À", "A"}, {"¥", "Y"}, {"ã", "a"}, {"Ã", "A"},
                                                                {"Ê", "E"}, {"Ë", "E"}, {"È", "E"}, {"Í", "I"},
                                                                {"Î", "I"}, {"Ï", "I"}, {"┌", "r"}, {"Ì", "I"},
                                                                {"Ó", "O"}, {"Ô", "O"}, {"Ò", "O"}, {"õ", "o"},
                                                                {"Õ", "O"}, {"Ú", "U"}, {"Û", "U"}, {"Ù", "U"},
                                                                {"ý", "Y"}, {"Ý", "Y"}}
        If ExtDictionary IsNot Nothing Then
            For Each item In ExtDictionary
                dictionary.Add(item.Key, item.Value)
            Next
        End If

        strRes = String.Join("", str.ToCharArray().Select(Function(x) If(dictionary.ContainsKey(x), dictionary(x), x)))
        Return strRes
    End Function

    ''' <summary>
    ''' Obtiene el timestamp de una fecha
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetTimestamp(value As DateTime) As Integer
        Return CInt(value.Subtract(New DateTime(1970, 1, 1)).TotalSeconds)
    End Function

    ''' <summary>
    ''' Retorna un simbolo de moneda dependiedo del codigo de la misma segun la ISO 4217
    ''' </summary>
    ''' <param name="ISO4217"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetCurrencySimbol(ISO4217 As String) As String
        Dim _region = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(Function(ct) New RegionInfo(ct.LCID)).
            Where(Function(ri) ri.ISOCurrencySymbol = ISO4217).FirstOrDefault
        Return If(_region Is Nothing, ISO4217, _region.CurrencySymbol)
    End Function

    ''' <summary>
    ''' recurso para retornar el ID de la cultura
    ''' </summary>
    ''' <param name="ISO4217"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetCultureId(ISO4217 As String) As Integer

        If String.IsNullOrEmpty(ISO4217) Then
            Return CultureInfo.CurrentCulture.LCID
        End If

        Dim cultureId As Integer
        Dim _region = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(Function(ct)
                                                                                        cultureId = ct.LCID
                                                                                        Return New RegionInfo(ct.LCID)
                                                                                    End Function).
            Where(Function(ri) ri.ISOCurrencySymbol = ISO4217).FirstOrDefault
        Return If(_region Is Nothing, CultureInfo.CurrentCulture.LCID, cultureId)
    End Function

    ''' <summary>
    ''' recurso para retornar el formato numerico de la moneda
    ''' </summary>
    ''' <param name="ISO4217"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetNumberFormat(ISO4217 As String) As NumberFormatInfo
        Dim indigo = SessionValues.Instance

        If String.IsNullOrEmpty(ISO4217) Then
            Return indigo.ConfigurationSeparatorNumberFormat(CultureInfo.CurrentCulture.NumberFormat)
        End If

        Dim culture = CultureInfo.CurrentCulture
        Dim _region = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(Function(ct)
                                                                                        If ct IsNot Nothing Then
                                                                                            culture = ct
                                                                                        End If
                                                                                        Return New RegionInfo(ct.LCID)
                                                                                    End Function).
                                                                                    Where(Function(ri) ri.ISOCurrencySymbol = ISO4217).FirstOrDefault

        Return indigo.ConfigurationSeparatorNumberFormat(culture.NumberFormat)
    End Function

#End Region

    <Extension()>
    Public Function AsInt(ByVal obj As [Object]) As Integer?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CInt(obj)
    End Function

    <Extension()>
    Public Function AsBoolean(ByVal obj As [Object]) As Boolean?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CBool(obj)
    End Function

    <Extension()>
    Public Function AsInt64(ByVal obj As [Object]) As Long?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return Convert.ToInt64(obj)
    End Function

    <Extension()>
    Public Function AsDecimal(ByVal obj As [Object]) As Decimal?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CDec(obj)
    End Function

    <Extension()>
    Public Function AsString(ByVal obj As Object) As String
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return Convert.ToString(obj)
    End Function

    <Extension()>
    Public Function AsDateTime(ByVal obj As Object) As DateTime?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CDate(obj)
    End Function

    <Extension()>
    Public Function AsDate(ByVal obj As Object) As Date?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CDate(obj).Date
    End Function

    <Extension()>
    Public Function AsByte(ByVal obj As Object) As Byte?
        If obj Is Nothing OrElse obj.Equals(DBNull.Value) Then
            Return Nothing
        End If
        Return CByte(obj)
    End Function

    <Extension()>
    Public Function AsInt(ByVal obj As Xml.XmlNode) As Integer?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return CInt(obj.InnerText)
    End Function

    <Extension()>
    Public Function AsBoolean(ByVal obj As Xml.XmlNode) As Boolean?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return CBool(obj.InnerText)
    End Function

    <Extension()>
    Public Function AsInt64(ByVal obj As Xml.XmlNode) As Long?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return Convert.ToInt64(obj.InnerText)
    End Function

    ''' <summary>
    ''' extension para convertir el valor de una etiqueta xml a decimal, la cultura del XML debe ser 'en-US' o invariante
    ''' Servidor SQL INDIGO tiene por defecto la cultura 'en-US'
    ''' </summary>
    ''' <param name="obj">decimal</param>
    ''' <returns></returns>
    <Extension()>
    Public Function AsDecimal(ByVal obj As Xml.XmlNode) As Decimal?
        If obj Is Nothing Then
            Return Nothing
        End If

        Return Decimal.Parse(obj.InnerText, Globalization.NumberStyles.AllowDecimalPoint, Globalization.CultureInfo.InvariantCulture)
    End Function

    <Extension()>
    Public Function AsString(ByVal obj As Xml.XmlNode) As String
        If obj Is Nothing Then
            Return Nothing
        End If
        Return Convert.ToString(obj.InnerText)
    End Function

    <Extension()>
    Public Function AsDateTime(ByVal obj As Xml.XmlNode) As DateTime?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return CDate(obj.InnerText)
    End Function

    <Extension()>
    Public Function AsDate(ByVal obj As Xml.XmlNode) As Date?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return CDate(obj.InnerText).Date
    End Function

    <Extension()>
    Public Function AsByte(ByVal obj As Xml.XmlNode) As Byte?
        If obj Is Nothing Then
            Return Nothing
        End If
        Return CByte(obj.InnerText)
    End Function

    ''' <summary>
    ''' Obtiene la descripcion de un enumerador, recupera la anotacion description
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetEnumDescription(ByVal value As [Enum]) As String
        Dim fi As System.Reflection.FieldInfo = value.[GetType]().GetField(value.ToString())
        Dim attributes As DescriptionAttribute() = CType(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            Return attributes(0).Description
        Else
            Return value.ToString()
        End If
    End Function

    <Extension()>
    Public Function GetRowSearch(gridView As GridView) As Object
        Dim obj As Object = gridView.GetFocusedRow()
        Do While TypeOf obj Is DevExpress.Data.NotLoadedObject
            System.Windows.Forms.Application.DoEvents()
            obj = gridView.GetFocusedRow()
        Loop
        Return obj
    End Function

#Region "Injector"
    Public Class ModuleInjector
        Public Sub Load(ByVal container As IUnityContainer, type As Type)
            Dim implementedTypes = AppDomain.CurrentDomain.GetAssemblies() _
                .Where(Function(m) m.FullName.StartsWith("Application.") _
                    OrElse m.FullName.StartsWith("Domain.") _
                    OrElse m.FullName.StartsWith("Infrastructure.Data.")) _
                .SelectMany(Function(m) m.GetLoadableTypes()) _
                .Where(Function(m) Not m.IsInterface AndAlso Not m.IsGenericType AndAlso type.IsAssignableFrom(m))

            For Each item As Type In implementedTypes
                Dim interfaces = item.GetInterfaces()? _
                    .Where(Function(m) Not m.Equals(type) AndAlso Not m.Equals(GetType(IDisposable)))? _
                    .ToList()
                If interfaces IsNot Nothing AndAlso interfaces.Any() Then
                    interfaces.ForEach(Sub(i) container.RegisterType(i, item))
                End If
            Next
        End Sub
    End Class
#End Region

#Region "Exceptions"
    <Extension()>
    Public Function ToDetailString(ex As Exception) As String
        If ex.GetType().Name.Equals("DbEntityValidationException") Then
            Dim errors As New Text.StringBuilder()
            For Each eve In CType(ex, Object).EntityValidationErrors
                errors.AppendLine(String.Format("Entity of type {0} in state {1} has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State))
                For Each ve In eve.ValidationErrors
                    errors.AppendLine(String.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage))
                Next
            Next
            Return errors.ToString()
        Else
            Dim properties = ex.[GetType]().GetProperties()
            Dim fields = properties.[Select](Function([property]) New With {
                Key .Name = [property].Name,
                Key .Value = [property].GetValue(ex, Nothing)
            }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
            Return [String].Join(vbLf, fields)
        End If
    End Function
#End Region

    <Extension()>
    Public Function Clone(Of TEntity)(ByVal obj As TEntity) As TEntity
        Dim o = obj.GetType()
        Dim nobj = Activator.CreateInstance(Of TEntity)()

        For Each i In o.GetProperties()
            Try
                i.SetValue(nobj, i.GetValue(obj))
            Catch
            End Try
        Next

        Return nobj
    End Function
End Module

