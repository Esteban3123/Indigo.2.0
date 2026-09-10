'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-08-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IDilutionFactorsAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Guarda  o actualiza
    ''' </summary>
    ''' <param name="ListDilutionFactors"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveDilutionFactorsRepositoryAsync(ListDilutionFactors As List(Of DilutionFactors), audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult)

    ''' <summary>
    ''' obtiene registro por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Function GetDilutionFactorsByCode(Code As String) As ActionResult(Of DilutionFactors)

End Interface
