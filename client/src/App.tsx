import { useEffect, useState } from "react";
import "./App.css";

type Store = {
  id?: string;
  name: string;
  opening_time?: string;
  closing_time?: string;
  is_open?: boolean;
  opening_hour: string;
  opening_minute: string;
  closing_hour: string;
  closing_minute: string;
};

function App() {
  const [stores, setStores] = useState<Store[]>([]);
  const [newStore, setNewStore] = useState<Store>({
    name: "",
    opening_hour: "9",
    opening_minute: "0",
    closing_hour: "5",
    closing_minute: "0",
  });
  const getStores = async () => {
    const response = await fetch("http://localhost:5068/api/stores");
    const data = await response.json();
    setStores(data);
  };

  const deleteStore = async (id: string) => {
    await fetch(`http://localhost:5068/api/stores/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    });
    getStores();
  };

  const addStore = async () => {
    await fetch("http://localhost:5068/api/stores", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newStore),
    });
    getStores();
  };

  const nameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewStore({ ...newStore, name: e.target.value });
  };

  const openingHourChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewStore({ ...newStore, opening_hour: e.target.value });
  };

  const openingMinuteChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewStore({ ...newStore, opening_minute: e.target.value });
  };

  const closingHourChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewStore({ ...newStore, closing_hour: e.target.value });
  };

  const closingMinuteChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewStore({ ...newStore, closing_minute: e.target.value });
  };

  useEffect(() => {
    getStores();
  }, []);

  return (
    <>
      <main>
        <h1>Coffee Shops</h1>
        <section>
          <h2>Stores</h2>
          <ul>
            {stores.map(({ id, name, opening_time, closing_time, is_open }) => (
              <li className="card" key={id}>
                <h3>{name}</h3>
                <h4>Opens: {opening_time}</h4>
                <h4>Closes: {closing_time}</h4>
                <h4>{is_open ? "Open" : "Closed"}</h4>
                {id ? (
                  <button onClick={() => deleteStore(id)}>Delete</button>
                ) : null}
              </li>
            ))}
          </ul>
        </section>
        <section>
          <div className="add-form">
            <label htmlFor="name">
              Name:
              <input type="text" value={newStore.name} onChange={nameChange} />
            </label>
            <label htmlFor="name">
              Opening Hour(24 hour format):
              <input
                type="text"
                value={newStore.opening_hour}
                onChange={openingHourChange}
              />
            </label>
            <label htmlFor="name">
              Opening Minute:
              <input
                type="text"
                value={newStore.opening_minute}
                onChange={openingMinuteChange}
              />
            </label>
            <label htmlFor="name">
              Closing Hour(24 hour format):
              <input
                type="text"
                value={newStore.closing_hour}
                onChange={closingHourChange}
              />
            </label>
            <label htmlFor="name">
              Closing Minute:
              <input
                type="text"
                value={newStore.closing_minute}
                onChange={closingMinuteChange}
              />
            </label>
          </div>
          <footer>
            <button onClick={addStore}>Add Store</button>
          </footer>
        </section>
      </main>
    </>
  );
}

export default App;
