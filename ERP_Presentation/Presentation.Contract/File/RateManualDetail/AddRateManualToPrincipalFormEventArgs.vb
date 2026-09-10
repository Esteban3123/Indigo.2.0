'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/11/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddRateManualToPrincipalFormEventArgs
    Inherits EventArgs

    Property RateManualDetail As RateManualDetail

    Property DeleteEntity As Boolean

    Property SaveModify As Boolean

End Class
