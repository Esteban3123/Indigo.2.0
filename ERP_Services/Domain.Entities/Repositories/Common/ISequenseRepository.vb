'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface ISequenseRepository
    Inherits IRepository(Of Sequense)

#Region "Methods"

    
    Function ListSequences() As List(Of Sequense)

    Function GetPatternSequence(idSeq As Integer) As Domain.Entities.Sequense

    Function GetPatternSequenceByName(nameSeq As String) As Domain.Entities.Sequense

#End Region

End Interface