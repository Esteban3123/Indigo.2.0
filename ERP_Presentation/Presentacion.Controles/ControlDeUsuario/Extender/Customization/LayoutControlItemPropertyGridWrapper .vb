'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-01-13
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2017-01-13
' Description      : Muestra una lista de propiedades disponibles
'                    en los LayoutControlItem
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.XtraLayout
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports Microsoft.VisualBasic

#End Region

''' <summary>
''' Crea un envoltorio para exponer alguna propiedades 
''' base y otras personalizadas, de un LayoutControlItem
''' </summary>
Public Class LayoutControlItemPropertyGridWrapper
    Inherits BasePropertyGridObjectWrapper

#Region "Fields"



#End Region

#Region "Properties Layout"

    ''' <summary>
    ''' Obtiene el LayoutControlItem
    ''' </summary>
    ''' <returns>LayoutControlItem</returns>
    <Category("1. LayoutControlItem"), Description("Expone las propiedades del LayoutControlItem")>
    Public ReadOnly Property Properties As LayoutControlItem
        Get
            Return DirectCast(Me.WrappedObject, LayoutControlItem)
        End Get
    End Property

#End Region

#Region "Properties Control"

    ' ''' <summary>
    ' ''' Obtiene o asigna la longitud de dato del control alojado en el LayoutControlItem
    ' ''' </summary>
    ' ''' <value>Longitud de dato usado en el control</value>
    ' ''' <returns>La longitud de dato usado en el control</returns>
    '<Category("2. Control"), Description("Obtiene o asigna la longitud de dato del control alojado en el LayoutControlItem")>
    'Public Property FieldLength As Int64
    '    Get
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            Return CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldLength
    '        Else
    '            Return -1
    '        End If
    '    End Get
    '    Set(value As Int64)
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldLength = value
    '        End If
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Obtiene o asigna el nombre del campo en la tabla
    ' ''' </summary>
    ' ''' <value>Nombre del campo en la tabla</value>
    ' ''' <returns>El nombre del campo en la tabla</returns>
    '<Category("2. Control"), Description("Obtiene o asigna el nombre del campo en la tabla")>
    'Public Property FieldName As String
    '    Get
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            If CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldName Is Nothing OrElse CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldName.ToString().Trim().Equals(String.Empty) Then
    '                CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldName = Me.Properties.Text.Replace(" ", "_")
    '            End If
    '            Return CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldName
    '        Else
    '            Return String.Empty
    '        End If
    '    End Get
    '    Set(value As String)
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldName = value
    '        End If
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Obtiene o asigna el tipo de dato del control alojado en el LayoutControlItem
    ' ''' </summary>
    ' ''' <value>Tipo de dato usado en el control</value>
    ' ''' <returns>El tipo de dato usado en el control</returns>
    '<Category("2. Control"), Description("Obtiene o asigna el tipo de dato del control alojado en el LayoutControlItem")>
    'Public Property FieldType As TypeCode
    '    Get
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            Return CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldType
    '        Else
    '            Return TypeCode.Empty
    '        End If
    '    End Get
    '    Set(value As TypeCode)
    '        If Me.Properties.Control.GetType().GetInterfaces().Any(Function(x) x.IsGenericType AndAlso x.GetGenericTypeDefinition().Equals(GetType(ICustom(Of )))) Then
    '            CTypeDynamic(Me.Properties.Control, Me.Properties.Control.GetType()).FieldType = value
    '        End If
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Obtiene o asigna la fuente usada en el control alojado
    ' ''' en el LayoutControlItem
    ' ''' </summary>
    ' ''' <value>Fuente a usar en el control</value>
    ' ''' <returns>La fuente usada en el control</returns>
    '<Category("2. Control"), Description("Obtiene o asigna la fuente usada en el control alojado en el LayoutControlItem")>
    'Public Property FontControl As Font
    '    Get
    '        Return Me.Properties.Control.Font
    '    End Get
    '    Set(value As Font)
    '        Me.Properties.Control.Font = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el valor del control alojado en el LayoutControlItem es opcional
    ''' </summary>
    ''' <value>Valor que indica si el control tiene un valor opcional</value>
    ''' <returns>Un valor que indica si el control tiene un valor opcional</returns>
    <Category("2. Control"), Description("Obtiene o asigna un valor que indica si el valor del control alojado en el LayoutControlItem es requerido")>
    Public Property IsRequired As Boolean
        Get
            Return Not Me.Properties.AllowHide
        End Get
        Set(value As Boolean)
            If Me.Properties.AllowHide <> Not value Then
                Me.Properties.AllowHide = Not value
                'Esto se realiza para garantizar que la propiedad IsRequired del
                'LayoutControl quede en True
                Me.Properties.Text = "_" & Me.Properties.Text
                Me.Properties.Text = Me.Properties.Text.Substring(1, Me.Properties.Text.Length - 1)
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera una copia del objeto
    ''' </summary>
    ''' <returns>Copia del objeto</returns>
    Public Overrides Function Clone() As BasePropertyGridObjectWrapper
        Return New LayoutControlItemPropertyGridWrapper()
    End Function

#End Region

End Class
