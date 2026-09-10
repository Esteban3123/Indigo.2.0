'***********************************************************************
' Assembly         : Application.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IFiscalizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Valida el archivo
    ''' </summary>
    Function SP_ValidateTaxBase(ListData As List(Of String), Year As Integer) As ActionResult(Of List(Of SP_ValidateTaxBase_Result))

End Interface
