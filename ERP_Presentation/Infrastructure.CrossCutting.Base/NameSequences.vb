'***********************************************************************
' Assembly         : Infraestructura.CrossCutting.Base
' Author           : Carlos Ernesto Cordoba
' Created          : 2014-03-11
'
' Description      : Clase usada para almacenar el nombre de las 
'                   secuencias utilizadas en las tablas que no tienen funcional
'
' Copyright        : (c) . All rights reserved.

Public NotInheritable Class NameSequences
    ''' <summary>
    ''' secuencia utilizada en las facturas de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INITIALBALANCE_ACCOUNTRECEIVABLE As String = "SICXC"
    ''' <summary>
    ''' secuencia utilizada en los anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INITIALBALANCE_PORTFOLIOADVANCE As String = "SIAC"
End Class
