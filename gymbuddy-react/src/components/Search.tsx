import { useState } from "react"

const Search = (props: { onSearch: (search: string) => void, onClear: () => void }) => {
    const [search, setSearch] = useState('')

    return (
        <div className="d-flex justify-content-center">
            <div className="input-group rounded col-5">
                <input type="search" className="form-control rounded" placeholder="Search" aria-label="Search"
                    aria-describedby="search-addon" value={search} onChange={(e) => setSearch(e.target.value)}/>
                <span onClick={() => props.onSearch(search)} style={{cursor: "pointer"}} className="input-group-text border-0 ml-3" id="search-addon">
                    <i className="fas fa-search"></i>
                </span>
                <span onClick={props.onClear} style={{cursor: "pointer"}} className="input-group-text border-0 ml-3" id="search-addon">
                    <i className="fas fa-times"></i>
                </span>
            </div>
        </div>
    )
}

export default Search
