using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, object> values;

        public BaseViewModel()
        {
            this.values = new Dictionary<string, object>();
        }
        /// <summary>
		/// 获得值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="property">属性（当前属性）</param>
		/// <returns></returns>
		protected T GetValue<T>(Expression<Func<T>> property)
        {
            if (property == null)
            {
                throw new ArgumentException("无效的视图模型属性定义.");
            }
            string propertyName = this.GetPropertyName(property);
            return this.GetValueInternal<T>(propertyName);
        }

        /// <summary>
		/// 当属性值更改时发生
		/// </summary>
		[method: CompilerGenerated]
        [CompilerGenerated]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
		/// 获得属性名称
		/// </summary>
		/// <param name="lambda">lambda表达式</param>
		/// <returns></returns>
		private string GetPropertyName(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                memberExpression = ((lambda.Body as UnaryExpression).Operand as MemberExpression);
            }
            else
            {
                memberExpression = (lambda.Body as MemberExpression);
            }
            Expression arg_37_0 = memberExpression.Expression;
            return (memberExpression.Member as PropertyInfo).Name;
        }

        /// <summary>
		/// 赋值（泛型）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="property">属性（当前属性）</param>
		/// <param name="value">值</param>
		protected void SetValue<T>(Expression<Func<T>> property, T value)
        {
            if (property == null)
            {
                throw new ArgumentException("无效的视图模型的属性定义.");
            }
            string propertyName = this.GetPropertyName(property);
            if (!object.Equals(this.GetValueInternal<T>(propertyName), value))
            {
                this.values[propertyName] = value;
                this.OnPropertyChanged(propertyName);
            }
        }

        private T GetValueInternal<T>(string propertyName)
        {
            object obj;
            if (this.values.TryGetValue(propertyName, out obj))
            {
                return (T)obj;
            }
            return default(T);
        }
    }
}
