'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-01-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2017-01-10
' Description      : Control de edición de texto
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.XtraEditors
Imports System.IO
Imports System.ComponentModel
Imports Presentation.Controls.ExtHelper

#End Region

''' <summary>
''' Control de edición de texto
''' </summary>
<Browsable(False)>
Public Class TextEditCustom
    Inherits TextEdit
    Implements ICustom(Of TextEditCustom)

#Region "ICustom"

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el código del tipo de dato usado para 
    ''' convertir el valor editable del control
    ''' </summary>
    ''' <value>Código del tipo de dato usado</value>
    ''' <returns>El código tipo de dato usado</returns>
    Public Property TypeOfData As TypeCode Implements ICustom(Of TextEditCustom).TypeOfData

#End Region

#Region "Methods"

    ''' <summary>
    ''' Graba los datos básicos del control en una corriente de datos binaria
    ''' </summary>
    ''' <param name="strm">Corriente de datos</param>
    Public Sub SaveToStream(strm As IO.Stream) Implements ICustom(Of TextEditCustom).SaveToStream
        Dim bw As New BinaryWriter(strm)
        bw.Write(Me.EditValue)
    End Sub

    ''' <summary>
    ''' Carga los datos básicos del control de una corriente de datos binaria
    ''' </summary>
    ''' <param name="strm">Corriente de datos</param>
    Public Sub LoadFromStream(strm As Stream) Implements ICustom(Of TextEditCustom).LoadFromStream
        Dim br As New BinaryReader(strm)
        Me.EditValue = ExtHelper.ReadOfType(br, ExtHelper.ToType(TypeOfData))
    End Sub

#End Region

#End Region

End Class
