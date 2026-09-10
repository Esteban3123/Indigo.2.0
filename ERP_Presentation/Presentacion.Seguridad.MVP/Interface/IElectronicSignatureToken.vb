'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Anthony Ocampo
' Created          : 26-03-2024
'
' Last Modified By : Anthony Ocampo
' Last Modified On : 26-03-2024
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
#End Region

Public Interface IElectronicSignatureToken

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el token de la firma electronica
    ''' </summary>
    ''' <returns></returns>
    Property Token As String

    ''' <summary>
    ''' Establece un mensaje en pantalla
    ''' </summary>
    WriteOnly Property Mensajes As String
#End Region

#Region "Methods"

#End Region

End Interface
