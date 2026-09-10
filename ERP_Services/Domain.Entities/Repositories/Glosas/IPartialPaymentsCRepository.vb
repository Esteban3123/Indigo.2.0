'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-06-28
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPartialPaymentsCRepository
    Inherits IRepository(Of PartialPaymentsC)

    ''' <summary>
    ''' Obtener un oficio de pagos parciales por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPartialPaymentsC(consecutive As String) As PartialPaymentsC

End Interface
