'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities


Public Interface IFiscalizationRepository
    Inherits IRepository(Of ThirdParty)

    ''' <summary>
    ''' Valida el archivo
    ''' </summary>
    Function SP_ValidateTaxBase(Xml As String, Year As Integer) As List(Of SP_ValidateTaxBase_Result)

End Interface
