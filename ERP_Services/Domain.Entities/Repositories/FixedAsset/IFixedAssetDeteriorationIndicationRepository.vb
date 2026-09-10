'************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-07-23
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region

Public Interface IFixedAssetDeteriorationIndicationRepository
    Inherits IRepository(Of DeteriorationIndications)

    ''' <summary>
    ''' Función que obtiene un indicio de deterioro por código
    ''' </summary>
    ''' <param name="Code">Código del indicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeteriorationIndicationByCode(Code As String) As DeteriorationIndications

    ''' <summary>
    ''' Función que obtiene todas los indicios de deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllDeteriorationIndications() As List(Of DeteriorationIndications)

End Interface
