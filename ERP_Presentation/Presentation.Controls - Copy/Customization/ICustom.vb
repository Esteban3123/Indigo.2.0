'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-01-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2017-01-10
' Description      : Define las caracteristicas de un control con 
'                    caracteristicas de customización
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.XtraEditors
Imports System.IO

#End Region

''' <summary>
''' Define las caracteristicas de un control con caracteristicas de customización
''' </summary>
''' <typeparam name="TImpl">Tipo quien implementa la interface</typeparam>
Public Interface ICustom(Of TImpl As {New, BaseEdit})

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el código del tipo de dato usado para 
    ''' convertir el valor editable del control
    ''' </summary>
    ''' <value>Código del tipo de dato usado</value>
    ''' <returns>El código tipo de dato usado</returns>
    Property TypeOfData As TypeCode

#End Region

#Region "Methods"

    ''' <summary>
    ''' Graba los datos básicos del control en una corriente de datos binaria
    ''' </summary>
    ''' <param name="strm">Corriente de datos</param>
    Sub SaveToStream(ByVal strm As Stream)

    ''' <summary>
    ''' Carga los datos básicos del control de una corriente de datos binaria
    ''' </summary>
    ''' <param name="strm">Corriente de datos</param>
    Sub LoadFromStream(ByVal strm As Stream)

#End Region

End Interface