using System;

// Step 1: Create a class to pass as an argument for the event handlers
public class TemperatureChangedEventArgs : EventArgs
{
    public double CurrentTemperature { get; set; }
}

// Step 2: Set up the delegate for the event
public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangedEventArgs e);

// Step 3: Declare the code for the event
public class WeatherStation
{
    // Event declaration
    public event TemperatureChangedEventHandler TemperatureChanged;

    private double _currentTemperature;

    public double CurrentTemperature
    {
        get { return _currentTemperature; }
        set
        {
            if (value != _currentTemperature)
            {
                _currentTemperature = value;
                // Step 5: Raise the event when the temperature changes
                OnTemperatureChanged(new TemperatureChangedEventArgs { CurrentTemperature = _currentTemperature });
            }
        }
    }

    // Step 5 (continued): Raise the event by invoking the delegate
    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
}

// Step 4: Create code that will be run when the event occurs (Event Handler)
public class TemperatureDisplay
{
    public void Subscribe(WeatherStation weatherStation)
    {
        // Step 5: Associate the event with the event handler
        weatherStation.TemperatureChanged += WeatherStation_TemperatureChanged;
    }

    // Event handler method
    private void WeatherStation_TemperatureChanged(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Current temperature is {e.CurrentTemperature} degrees Celsius.");
    }
}
