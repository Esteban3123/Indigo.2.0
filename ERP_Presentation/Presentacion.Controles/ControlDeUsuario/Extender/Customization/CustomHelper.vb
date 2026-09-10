'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-01-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2017-01-10
' Description      : Expone servicios de apoyo al proceso de customización
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System
Imports System.Reflection
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic

#End Region

''' <summary>
''' Expone servicios de apoyo al proceso de customización
''' </summary>
Public NotInheritable Class CustomHelper

#Region "Customization"

    ''' <summary>
    ''' Obtiene un diccionario de los controles disponibles en el ensamblado
    ''' para mostrar en el formulario de personalización
    ''' </summary>
    ''' <param name="suffix">Valor que indica si se elimina el sufijo del nombre del control</param>
    ''' <returns>Diccionario de tipos</returns>
    Public Shared Function ListCustomControls(Optional ByVal suffix As Boolean = False) As Dictionary(Of String, Type)
        Dim asm As Assembly = GetType(CustomHelper).Assembly
        Dim dic As New Dictionary(Of String, Type)()
        For Each t As Type In asm.GetTypes()
            'Dim exists = (From i As Type In t.GetInterfaces() Where i.Name.Equals("ICustom`1") Select i).Any()
            Dim exists = t.GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of ))))
            If exists Then
                If Not suffix Then
                    dic.Add(t.Name.Replace("Custom", ""), t)
                Else
                    dic.Add(t.Name, t)
                End If
            End If
        Next
        Return dic
    End Function

#End Region

#Region "Serialization"

    ''' <summary>
    ''' Constante que representa un valor nulo
    ''' </summary>
    Public Const NULL As String = "[|NULL|]"

    ''' <summary>
    ''' Convierte un MemoryStream a una cadena en base 64
    ''' </summary>
    ''' <param name="stream">Stream a convertir en cadena</param>
    ''' <returns>String resultado</returns>
    Public Shared Function StreamToBase64String(ByVal stream As MemoryStream) As String
        Return Convert.ToBase64String(stream.ToArray())
    End Function

    ''' <summary>
    ''' Convierte una cadena en base 64 a un Stream
    ''' </summary>
    ''' <param name="base64String">Cadena a convertir</param>
    ''' <returns>Stream resultado</returns>
    Public Shared Function Base64StringToStream(ByVal base64String As String) As Stream
        Return New MemoryStream(Convert.FromBase64String(base64String))
    End Function

    ''' <summary>
    ''' Serializa un objeto con un metodo especial
    ''' </summary>
    ''' <param name="obj">Objeto a serializar</param>
    ''' <returns>Cadena en base 64 resultado de la serialización</returns>
    Public Shared Function Serialize(ByVal obj As Object) As String
        If obj IsNot Nothing Then
            Dim spSerializer As New SpecialSerializer()
            Dim sb As New StringBuilder()
            For Each prop As PropertyInfo In obj.GetType().GetProperties().Where(Function(p) Not p.PropertyType.Equals(GetType(IntPtr)) And Not p.PropertyType.Equals(GetType(UIntPtr)) And p.CanRead)
                If prop.PropertyType.IsPrimitive OrElse prop.PropertyType.Equals(GetType(String)) Then
                    If sb.Length <= 0 Then
                        sb.Append(prop.Name & ":" & prop.GetValue(obj))
                    Else
                        sb.Append(";" & prop.Name & ":" & prop.GetValue(obj))
                    End If
                Else
                    If spSerializer.CanSerialize(prop.PropertyType) Then
                        If sb.Length <= 0 Then
                            sb.Append(prop.Name & ":" & spSerializer.Serialize(prop.GetValue(obj)))
                        Else
                            sb.Append(";" & prop.Name & ":" & spSerializer.Serialize(prop.GetValue(obj)))
                        End If
                    End If
                End If
            Next
            Return Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()))
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Deserializa un objeto con un metodo especial
    ''' </summary>
    ''' <param name="obj">Objeto a deserializar</param>
    ''' <param name="body">Cadena en base 64 del cuerpo del objeto serializado</param>
    Public Shared Sub Deserialize(ByRef obj As Object, ByVal body As String)
        If obj IsNot Nothing AndAlso body IsNot Nothing AndAlso Not body.Equals(String.Empty) Then
            Dim spSerializer As New SpecialSerializer()
            Dim props() As String = Encoding.UTF8.GetString(Convert.FromBase64String(body.Trim())).Split(";")
            If props IsNot Nothing AndAlso props.Length > 0 Then
                For Each prop As String In props
                    Dim keyValue() As String = prop.Split(":")
                    If keyValue IsNot Nothing AndAlso keyValue.Length = 2 Then
                        Dim pinfo As PropertyInfo = obj.GetType().GetProperty(keyValue(0))
                        If pinfo IsNot Nothing AndAlso pinfo.CanRead Then
                            If pinfo.CanWrite Then
                                If pinfo.PropertyType.IsPrimitive OrElse pinfo.PropertyType.Equals(GetType(String)) Then
                                    pinfo.SetValue(obj, CTypeDynamic(keyValue(1), pinfo.PropertyType))
                                Else
                                    If spSerializer.CanSerialize(pinfo.PropertyType) Then
                                        pinfo.SetValue(obj, spSerializer.Deserialize(pinfo.PropertyType, keyValue(1)))
                                    End If
                                End If
                            ElseIf pinfo.GetValue(obj) IsNot Nothing Then
                                If spSerializer.CanSerialize(pinfo.PropertyType) Then
                                    CustomHelper.SetProperties(pinfo.GetValue(obj), spSerializer.Deserialize(pinfo.PropertyType, keyValue(1)))
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna todas los valores de un objeto raiz a un objeto destino.
    ''' Ambos objeto deben ser del mismo tipo
    ''' </summary>
    ''' <param name="objDest">Objeto destino</param>
    ''' <param name="objValue">Objeto raiz</param>
    Public Shared Sub SetProperties(ByRef objDest As Object, ByVal objValue As Object)
        If objDest IsNot Nothing AndAlso objValue IsNot Nothing AndAlso objDest.GetType().Equals(objValue.GetType()) Then
            For Each prop As PropertyInfo In objDest.GetType().GetProperties()
                If Not prop.PropertyType.Equals(objDest.GetType()) Then
                    Dim p As PropertyInfo = objValue.GetType().GetProperty(prop.Name)
                    If p IsNot Nothing AndAlso p.CanRead Then
                        If prop.CanWrite Then
                            prop.SetValue(objDest, p.GetValue(objValue))
                        ElseIf prop.CanRead Then
                            CustomHelper.SetProperties(prop.GetValue(objDest), p.GetValue(objValue))
                        End If
                    End If
                End If
            Next
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Provee metodos extendidos para distintos tipos usados
''' en el proceso de customización
''' </summary>
Public Module ExtHelper

    ''' <summary>
    ''' Convierte el TypeCode a su correspondiente tipo de dato
    ''' </summary>
    ''' <param name="t">Tipo que extiende el metodo</param>
    ''' <returns>El tipo de dato al que corresponde el TypeCode</returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function ToType(ByVal t As TypeCode) As Type
        Select Case t
            Case TypeCode.[Boolean]
                Return GetType(Boolean)

            Case TypeCode.[Byte]
                Return GetType(Byte)

            Case TypeCode.[Char]
                Return GetType(Char)

            Case TypeCode.DateTime
                Return GetType(DateTime)

            Case TypeCode.DBNull
                Return GetType(DBNull)

            Case TypeCode.[Decimal]
                Return GetType(Decimal)

            Case TypeCode.[Double]
                Return GetType(Double)

            Case TypeCode.Empty
                Return Nothing

            Case TypeCode.Int16
                Return GetType(Short)

            Case TypeCode.Int32
                Return GetType(Integer)

            Case TypeCode.Int64
                Return GetType(Long)

            Case TypeCode.[Object]
                Return GetType(Object)

            Case TypeCode.[SByte]
                Return GetType(SByte)

            Case TypeCode.[Single]
                Return GetType([Single])

            Case TypeCode.[String]
                Return GetType(String)

            Case TypeCode.UInt16
                Return GetType(UInt16)

            Case TypeCode.UInt32
                Return GetType(UInt32)

            Case TypeCode.UInt64
                Return GetType(UInt64)
        End Select

        Return Nothing
    End Function

    ''' <summary>
    ''' Lee un tipo de dato <paramref name="T" /> de una corriente de bytes
    ''' </summary>
    ''' <param name="e">Tipo que extiende el metodo</param>
    ''' <param name="T">Tipo de dato a leer</param>
    ''' <returns>El valor leido</returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function ReadOfType(ByVal e As BinaryReader, ByVal T As Type) As Object
        Select Case Type.GetTypeCode(T)
            Case TypeCode.[Boolean]
                Return e.ReadBoolean
            Case TypeCode.[Byte]
                Return e.ReadByte
            Case TypeCode.[Char]
                Return e.ReadChar
            Case TypeCode.DateTime
                Return New DateTime(e.ReadInt64)
            Case TypeCode.DBNull
                Return Nothing
            Case TypeCode.[Decimal]
                Return e.ReadDecimal
            Case TypeCode.[Double]
                Return e.ReadDouble
            Case TypeCode.Empty
                Return Nothing
            Case TypeCode.Int16
                Return e.ReadInt16
            Case TypeCode.Int32
                Return e.ReadInt32
            Case TypeCode.Int64
                Return e.ReadInt64
            Case TypeCode.[Object]
                Return Nothing
            Case TypeCode.[SByte]
                Return e.ReadSByte
            Case TypeCode.[Single]
                Return e.ReadSingle
            Case TypeCode.[String]
                Return e.ReadString
            Case TypeCode.UInt16
                Return e.ReadUInt16
            Case TypeCode.UInt32
                Return e.ReadUInt32
            Case TypeCode.UInt64
                Return e.ReadUInt64
            Case Else
                Return Nothing
        End Select
    End Function

End Module

''' <summary>
''' Provee un metodo personalizado de serialización
''' </summary>
Public Class SpecialSerializer

#Region "Fields"

    ''' <summary>
    ''' Disccionario de serializadores
    ''' </summary>
    Private ReadOnly Serializers As New Dictionary(Of Type, Tuple(Of Func(Of Object, String), Func(Of String, Object)))()

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una mueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Serializers.Add(GetType(Image), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                           If obj IsNot Nothing Then
                                                                                                               Dim ms As New MemoryStream()
                                                                                                               CType(obj, Image).Save(ms, CType(obj, Image).RawFormat)
                                                                                                               Return CustomHelper.StreamToBase64String(ms)
                                                                                                           Else
                                                                                                               Return CustomHelper.NULL
                                                                                                           End If
                                                                                                       End Function,
                                                                                                       Function(body)
                                                                                                           If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                               Return Image.FromStream(CustomHelper.Base64StringToStream(body))
                                                                                                           Else
                                                                                                               Return Nothing
                                                                                                           End If
                                                                                                       End Function))
        Serializers.Add(GetType(Bitmap), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                            If obj IsNot Nothing Then
                                                                                                                Dim ms As New MemoryStream()
                                                                                                                CType(obj, Image).Save(ms, CType(obj, Image).RawFormat)
                                                                                                                Return CustomHelper.StreamToBase64String(ms)
                                                                                                            Else
                                                                                                                Return CustomHelper.NULL
                                                                                                            End If
                                                                                                        End Function,
                                                                                                       Function(body)
                                                                                                           If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                               Return Image.FromStream(CustomHelper.Base64StringToStream(body))
                                                                                                           Else
                                                                                                               Return Nothing
                                                                                                           End If
                                                                                                       End Function))
        Serializers.Add(GetType(DevExpress.Utils.TextOptions), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                                                  If obj IsNot Nothing Then
                                                                                                                                      Dim topt As DevExpress.Utils.TextOptions = CType(obj, DevExpress.Utils.TextOptions)
                                                                                                                                      Return Convert.ToBase64String(Encoding.UTF8.GetBytes(topt.HAlignment.ToString() & ":" & topt.HotkeyPrefix.ToString() & ":" & topt.Trimming.ToString() & ":" & topt.VAlignment.ToString() & ":" & topt.WordWrap.ToString()))
                                                                                                                                  Else
                                                                                                                                      Return CustomHelper.NULL
                                                                                                                                  End If
                                                                                                                              End Function,
                                                                                                                              Function(body)
                                                                                                                                  If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                                                      Dim props() As String = Encoding.UTF8.GetString(Convert.FromBase64String(body)).Split(":")
                                                                                                                                      If props IsNot Nothing AndAlso props.Length = 5 Then
                                                                                                                                          Return New DevExpress.Utils.TextOptions(CTypeDynamic([Enum].Parse(GetType(DevExpress.Utils.HorzAlignment), props(0)), GetType(DevExpress.Utils.HorzAlignment)), CTypeDynamic([Enum].Parse(GetType(DevExpress.Utils.VertAlignment), props(3)), GetType(DevExpress.Utils.VertAlignment)), CTypeDynamic([Enum].Parse(GetType(DevExpress.Utils.WordWrap), props(4)), GetType(DevExpress.Utils.WordWrap)), CTypeDynamic([Enum].Parse(GetType(DevExpress.Utils.Trimming), props(2)), GetType(DevExpress.Utils.Trimming)), CTypeDynamic([Enum].Parse(GetType(DevExpress.Utils.HKeyPrefix), props(1)), GetType(DevExpress.Utils.HKeyPrefix)))
                                                                                                                                      Else
                                                                                                                                          Return New DevExpress.Utils.TextOptions(DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, DevExpress.Utils.WordWrap.Default, DevExpress.Utils.Trimming.Default, DevExpress.Utils.HKeyPrefix.Default)
                                                                                                                                      End If
                                                                                                                                  Else
                                                                                                                                      Return Nothing
                                                                                                                                  End If
                                                                                                                              End Function))
        Serializers.Add(GetType(Color), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                           If obj IsNot Nothing Then
                                                                                                               Return CType(obj, Color).ToArgb.ToString()
                                                                                                           Else
                                                                                                               Return CustomHelper.NULL
                                                                                                           End If
                                                                                                       End Function,
                                                                                                       Function(body)
                                                                                                           If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                               Return Color.FromArgb(body)
                                                                                                           Else
                                                                                                               Return Color.Empty
                                                                                                           End If
                                                                                                       End Function))
        Serializers.Add(GetType(Font), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                          If obj IsNot Nothing Then
                                                                                                              Dim f As Font = CType(obj, Font)
                                                                                                              Return Convert.ToBase64String(Encoding.UTF8.GetBytes((If(f.Name IsNot Nothing, f.Name.Trim(), String.Empty) & ":" & f.Size.ToString() & ":" & f.Bold.ToString() & ":" & f.Italic.ToString() & ":" & f.Strikeout.ToString() & ":" & f.Underline.ToString())))
                                                                                                          Else
                                                                                                              Return CustomHelper.NULL
                                                                                                          End If
                                                                                                      End Function,
                                                                                                       Function(body)
                                                                                                           If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                               Dim props() As String = Encoding.UTF8.GetString(Convert.FromBase64String(body)).Split(":")
                                                                                                               If props IsNot Nothing AndAlso props.Length = 6 Then
                                                                                                                   If CBool(props(2)) And CBool(props(3)) And CBool(props(4)) And CBool(props(5)) Then '1 1 1 1
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                                                                                                                   ElseIf Not CBool(props(2)) And CBool(props(3)) And CBool(props(4)) And CBool(props(5)) Then '0 1 1 1
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                                                                                                                   ElseIf Not CBool(props(2)) And Not CBool(props(3)) And CBool(props(4)) And CBool(props(5)) Then '0 0 1 1
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Strikeout Or FontStyle.Underline)
                                                                                                                   ElseIf Not CBool(props(2)) And Not CBool(props(3)) And Not CBool(props(4)) And CBool(props(5)) Then '0 0 0 1
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Underline)
                                                                                                                   ElseIf Not CBool(props(2)) And Not CBool(props(3)) And Not CBool(props(4)) And Not CBool(props(5)) Then '0 0 0 0
                                                                                                                       Return New Font(props(0), CSng(props(1)))
                                                                                                                   ElseIf CBool(props(2)) And Not CBool(props(3)) And Not CBool(props(4)) And Not CBool(props(5)) Then '1 0 0 0
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Bold)
                                                                                                                   ElseIf CBool(props(2)) And CBool(props(3)) And Not CBool(props(4)) And Not CBool(props(5)) Then '1 1 0 0
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Bold Or FontStyle.Italic)
                                                                                                                   ElseIf CBool(props(2)) And CBool(props(3)) And CBool(props(4)) And Not CBool(props(5)) Then '1 1 1 0
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                                                                                                                   ElseIf Not CBool(props(2)) And CBool(props(3)) And Not CBool(props(4)) And Not CBool(props(5)) Then '0 1 0 0
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Italic)
                                                                                                                   Else '0 0 1 0
                                                                                                                       Return New Font(props(0), CSng(props(1)), FontStyle.Strikeout)
                                                                                                                   End If
                                                                                                               Else
                                                                                                                   Return DevExpress.XtraEditors.FontEdit.DefaultFont
                                                                                                               End If
                                                                                                           Else
                                                                                                               Return DevExpress.XtraEditors.FontEdit.DefaultFont
                                                                                                           End If
                                                                                                       End Function))
        Serializers.Add(GetType(DevExpress.Utils.AppearanceObject), New Tuple(Of Func(Of Object, String), Func(Of String, Object))(Function(obj)
                                                                                                                                       If obj IsNot Nothing Then
                                                                                                                                           Dim ap As DevExpress.Utils.AppearanceObject = CType(obj, DevExpress.Utils.AppearanceObject)
                                                                                                                                           Return Convert.ToBase64String(Encoding.UTF8.GetBytes(Serializers(GetType(Color)).Item1(ap.BackColor) & ":" & Serializers(GetType(Color)).Item1(ap.BackColor2) & ":" & Serializers(GetType(Color)).Item1(ap.BorderColor) & ":" & If(ap.Font IsNot Nothing, Serializers(GetType(Font)).Item1(ap.Font), String.Empty) & ":" & Serializers(GetType(Color)).Item1(ap.ForeColor) & ":" & If(ap.Name IsNot Nothing, ap.Name.Trim(), String.Empty) & ":" & If(ap.TextOptions IsNot Nothing, Serializers(GetType(DevExpress.Utils.TextOptions)).Item1(ap.TextOptions), String.Empty) & ":" & If(ap.Image IsNot Nothing, Serializers(GetType(Image)).Item1(ap.Image), String.Empty)))
                                                                                                                                       Else
                                                                                                                                           Return CustomHelper.NULL
                                                                                                                                       End If
                                                                                                                                   End Function,
                                                                                                                                   Function(body)
                                                                                                                                       If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) AndAlso Not body.Trim().Equals(CustomHelper.NULL) Then
                                                                                                                                           Dim props() As String = Encoding.UTF8.GetString(Convert.FromBase64String(body)).Split(":")
                                                                                                                                           If props IsNot Nothing AndAlso props.Length = 8 Then
                                                                                                                                               Dim ap As New DevExpress.Utils.AppearanceObject()
                                                                                                                                               ap.BackColor = Serializers(GetType(Color)).Item2()(props(0))
                                                                                                                                               ap.BackColor2 = Serializers(GetType(Color)).Item2()(props(1))
                                                                                                                                               ap.BorderColor = Serializers(GetType(Color)).Item2()(props(2))
                                                                                                                                               ap.Font = Serializers(GetType(Font)).Item2()(props(3))
                                                                                                                                               ap.ForeColor = Serializers(GetType(Color)).Item2()(props(4))
                                                                                                                                               ap.Name = props(5)
                                                                                                                                               Dim topt As DevExpress.Utils.TextOptions = Serializers(GetType(DevExpress.Utils.TextOptions)).Item2()(props(6))
                                                                                                                                               If topt IsNot Nothing Then
                                                                                                                                                   ap.TextOptions.HAlignment = topt.HAlignment
                                                                                                                                                   ap.TextOptions.HotkeyPrefix = topt.HotkeyPrefix
                                                                                                                                                   ap.TextOptions.Trimming = topt.Trimming
                                                                                                                                                   ap.TextOptions.VAlignment = topt.VAlignment
                                                                                                                                                   ap.TextOptions.WordWrap = topt.WordWrap
                                                                                                                                               End If
                                                                                                                                               ap.Image = Serializers(GetType(Image)).Item2()(props(7))
                                                                                                                                               Return ap
                                                                                                                                           Else
                                                                                                                                               Return New DevExpress.Utils.AppearanceObject()
                                                                                                                                           End If
                                                                                                                                       Else
                                                                                                                                           Return New DevExpress.Utils.AppearanceObject()
                                                                                                                                       End If
                                                                                                                                   End Function))
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Indica si el tipo puede ser serializado
    ''' </summary>
    ''' <param name="type">Tipo a serializar</param>
    ''' <returns>Valor que indica si se puede serializar el tipo</returns>
    Public Function CanSerialize(type As Type) As Boolean
        Return Serializers.ContainsKey(type)
    End Function

    ''' <summary>
    ''' Serializa un tipo
    ''' </summary>
    ''' <param name="target">Tipo a serializar</param>
    Public Function Serialize(target As Object) As String
        If target Is Nothing Then
            Return CustomHelper.NULL
        ElseIf CanSerialize(target.GetType()) Then
            Return Serializers(target.GetType()).Item1()(target)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Deserializa un tipo
    ''' </summary>
    ''' <param name="type">Tipo a deserializar</param>
    ''' <param name="body">Corriente de bytes de la que se lee el objeto</param>
    ''' <returns>Objeto deserializado</returns>
    Public Function Deserialize(type As Type, body As String) As Object
        If body IsNot Nothing AndAlso body.Trim().Equals(CustomHelper.NULL) Then
            Return Nothing
        ElseIf Not CanSerialize(type) Then
            Return Nothing
        End If
        Return Serializers(type).Item2()(body)
    End Function

#End Region

End Class