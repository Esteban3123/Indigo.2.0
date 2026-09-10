'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : jorge leonardo vernaza becerra
' Created          : 09-11-2011
'
' Last Modified By : jorge leonardo vernaza becerra
' Last Modified On : 09-11-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Class MCtrBiometricoInicio

#Region "Fields"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance

#End Region

    ''' <summary>
    ''' Funcion que retorna una lista con todos los registros de la table persona
    ''' </summary>
    ''' <returns></returns>
    Public Function ListarTodaslashuellas() As List(Of Person)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFingerPrint(Me._indigoSession)
    End Function
End Class
