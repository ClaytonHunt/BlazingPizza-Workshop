using BlazingPizza.Shared;
using System;
using System.Collections.Generic;

namespace BlazingPizza.Client.AppState
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public Pizza ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();
        public event EventHandler StateChanged;

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };

            ShowingConfigureDialog = true;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;

            ShowingConfigureDialog = false;
            StateHasChanged();
        }

        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
            StateHasChanged();
        }

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
            StateHasChanged();
        }

        public void ResetOrder()
        {
            Order = new Order();
        }

        public void AddTopping(Topping topping)
        {
            if (ConfiguringPizza.Toppings.Find(pt => pt.Topping == topping) == null)
            {
                ConfiguringPizza.Toppings.Add(new PizzaTopping() { Topping = topping });
            }
        }

        public void RemoveTopping(Topping topping)
        {
            ConfiguringPizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }

        private void StateHasChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
