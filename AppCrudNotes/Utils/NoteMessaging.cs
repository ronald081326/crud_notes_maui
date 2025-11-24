using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCrudNotes.Utils
{
    internal class NoteMessaging: ValueChangedMessage<NoteMessage>
    {

        public NoteMessaging(NoteMessage value): base(value) { 
        }
    }
}
