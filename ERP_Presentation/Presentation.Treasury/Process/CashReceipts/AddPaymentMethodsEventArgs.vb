'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 05-07-2014
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

Public Class AddPaymentMethodsEventArgs
    Inherits EventArgs

    Property PaymentMethods As PaymentMethods

End Class
