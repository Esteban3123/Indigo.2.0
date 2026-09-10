Imports System.Runtime.CompilerServices
Imports Domain.Base.Entities
Imports Domain.Entities

Module UsersAssignmentExtensions

    ''' <summary>
    ''' Función de extensión para clonar UsersAssignment y modificar su entrytype de manera encapsulada
    ''' </summary>
    ''' <param name="original"></param>
    ''' <param name="newEntryType"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function CloneWithNewEntryType(original As UsersAssignment, newEntryType As Integer) As UsersAssignment
        Dim user = New UsersAssignment With {
            .Id = original.Id,
            .UserCode = original.UserCode,
            .FullName = original.FullName,
            .EntryType = newEntryType,
            .Status = original.Status,
            .ChangeTracker = New ObjectChangeTracker()
        }
        If original.ChangeTracker.State = ObjectState.Unchanged Then
            user.MarkAsUnchanged()
        ElseIf original.ChangeTracker.State = ObjectState.Added Then
            user.MarkAsAdded
        ElseIf original.ChangeTracker.State = ObjectState.Modified Then
            user.MarkAsModified()
        End If
        Return user
    End Function

End Module
