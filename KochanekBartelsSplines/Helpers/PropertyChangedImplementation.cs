using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KochanekBartelsSplines.Helpers
{
    public class PropertyChangedImplementation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            var memberExpression = (MemberExpression) propertyExpression.Body;

            var propertyName = memberExpression.Member.Name;

            RaisePropertyChanged(propertyName);
        }
    }
}
